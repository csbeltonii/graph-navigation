﻿using GraphSurvey.GraphModel;
using static System.Console;

namespace GraphSurvey.SurveyModel;

public class Survey(List<Question> questions, Graph<SurveyObjectMetaData> surveyGraph)
{
    public Dictionary<string, Question> Questions { get; set; } =
        questions.ToDictionary(question => question.Name, question => question);

    public Graph<SurveyObjectMetaData> SurveyGraph { get; set; } = surveyGraph;

    public SurveyQuota? SurveyQuota { get; set; }

    private Question? _currentQuestion;
    private Node<SurveyObjectMetaData>? _currentNode;
    private int _currentIndex;
    private int _currentResponse;
    private Choice? _currentChoice;

    /// <summary>
    /// Navigates the survey based on the list of questions.
    /// </summary>
    /// <returns></returns>
    public SurveyResult TakeSurveyV1()
    {
        if (Questions.Count == 0)
        {
            return SurveyResult.EndOfSurvey();
        }

        var hasNextQuestion = true;
        var questions = Questions.Values.ToList();
        _currentQuestion = questions[0];

        while (hasNextQuestion)
        {
            _currentQuestion!.Display();

            if (_currentQuestion.Choices.Count > 0)
            {
                var input = int.Parse(ReadLine()!);

                if (input == 0)
                {
                    if (_currentIndex == 0)
                    {
                        continue;
                    }

                    _currentIndex--;
                    _currentQuestion = questions[_currentIndex];

                    continue;
                }

                var choiceValue = _currentQuestion.Choices.FirstOrDefault(choice => choice.Value.Equals(input))!;
                var choiceDslValue = choiceValue.DslValue(_currentQuestion.Name);

                if (choiceValue.IsTerminate)
                {
                    return SurveyResult.Terminated();
                }

                if (SurveyQuota is { QuotaCells.Count: > 0 })
                {
                    var quotaCell = SurveyQuota.QuotaCells.FirstOrDefault(cell => cell.Condition == choiceDslValue);

                    if (quotaCell != null)
                    {
                        if (quotaCell.Filled)
                        {
                            return SurveyResult.OverQuota();
                        }

                        quotaCell.Count++;
                    }
                }

                var node = SurveyGraph.Nodes.FirstOrDefault(node => node.GraphData.Name == _currentQuestion.Name)!;

                var navigateTo = node
                                 .NavigationConditions
                                 .FirstOrDefault(condition => condition.Value == choiceValue.Name)?
                                 .TargetNode;

                if (navigateTo is null)
                {
                    _currentIndex++;

                    if (Questions.Count >= _currentIndex)
                    {
                        _currentQuestion = questions[_currentIndex];
                        continue;
                    }

                    hasNextQuestion = false;
                }
                else
                {
                    _currentQuestion = questions.First(q => q.Name == navigateTo);
                    _currentIndex = questions.IndexOf(_currentQuestion);
                }
            }
            else
            {
                hasNextQuestion = false;
            }
        }

        return SurveyResult.EndOfSurvey();
    }

    /// <summary>
    /// Navigates the survey based on the nodes, their navigation conditions, and question lookup.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public SurveyResult TakeSurveyV2()
    {
        if (SurveyGraph.NodeCount == 0)
        {
            return SurveyResult.EndOfSurvey();
        }

        var continueExecution = true;

        _currentNode = SurveyGraph.Nodes[0];
        _currentIndex = 0;

        while (continueExecution)
        {
            switch (_currentNode?.GraphData.Type)
            {
                case "question":
                    _currentQuestion = Questions[_currentNode.GraphData.Name];
                    _currentQuestion.Display();

                    // Base case - if there are no questions, break the infinite loop
                    if (_currentQuestion.Choices.Count == 0)
                    {
                        continueExecution = false;
                        break;
                    }

                    _currentResponse = int.Parse(ReadLine()!);

                    _currentChoice = _currentQuestion.Choices.FirstOrDefault(choice => choice.Value.Equals(_currentResponse))!;

                    if (_currentChoice.IsTerminate)
                    {
                        return SurveyResult.Terminated();
                    }

                    if (_currentChoice.CompleteAfter)
                    {
                        return SurveyResult.EndOfSurvey();
                    }

                    var navigateTo = _currentNode
                                     .NavigationConditions
                                     .FirstOrDefault(condition => condition.Value == _currentChoice.Name)?
                                     .TargetNode;

                    if (navigateTo is null)
                    {
                        _currentIndex++;
                        _currentNode = SurveyGraph.Nodes[_currentIndex];

                        _currentQuestion = Questions[_currentNode.GraphData.Name];
                    }
                    else
                    {
                        _currentNode = SurveyGraph.Nodes.FirstOrDefault(node => node.GraphData.Name == navigateTo);
                    }

                    if (_currentIndex == SurveyGraph.NodeCount)
                    {
                        continueExecution = false;
                    }

                    break;
                case "quota":
                    WriteLine("Passed Quota Marker");

                    var lastQuestion = Questions[SurveyGraph.Nodes[_currentNode.Index - 1].GraphData.Name];
                    var choiceDslValue = _currentChoice?.DslValue(lastQuestion!.Name);

                    if (SurveyQuota is { QuotaCells.Count: > 0 })
                    {
                        var quotaCell = SurveyQuota.QuotaCells.FirstOrDefault(cell => cell.Condition == choiceDslValue);

                        if (quotaCell != null)
                        {
                            if (quotaCell.Filled)
                            {
                                return SurveyResult.OverQuota();
                            }

                            quotaCell.Count++;
                        }
                    }

                    _currentIndex++;

                    if (_currentIndex == SurveyGraph.NodeCount)
                    {
                        continueExecution = false;
                    }

                    _currentNode = SurveyGraph.Nodes.FirstOrDefault(
                        node => node.GraphData == _currentNode.Neighbors.FirstOrDefault()!.GraphData
                    );

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            WriteLine();
        }


        return SurveyResult.EndOfSurvey();
    }

    public void SetQuota(SurveyQuota surveyQuota) => SurveyQuota = surveyQuota;
}