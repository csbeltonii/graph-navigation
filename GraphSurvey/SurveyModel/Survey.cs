using GraphSurvey.GraphModel;

namespace GraphSurvey.SurveyModel;

public class Survey(List<Question> questions, Graph<SurveyObjectMetaData> surveyGraph)
{
    public List<Question> Questions { get; set; } = questions;
    public Graph<SurveyObjectMetaData> SurveyGraph { get; set; } = surveyGraph;

    public SurveyQuota? SurveyQuota { get; set; }

    private Question? _currentQuestion;
    private int _currentIndex;

    public SurveyResult TakeSurvey()
    {
        if (Questions.Count == 0)
        {
            return SurveyResult.EndOfSurvey();
        }

        var hasNextQuestion = true;
        _currentQuestion = Questions[0];

        while (hasNextQuestion)
        {
            _currentQuestion!.Display();

            if (_currentQuestion.Choices.Count > 0)
            {
                var input = int.Parse(Console.ReadLine()!);

                if (input == 0)
                {
                    if (_currentIndex == 0)
                    {
                        continue;
                    }

                    _currentIndex--;
                    _currentQuestion = Questions[_currentIndex];

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
                        _currentQuestion = Questions[_currentIndex];
                        continue;
                    }

                    hasNextQuestion = false;
                }
                else
                {
                    _currentQuestion = Questions.First(q => q.Name == navigateTo);
                    _currentIndex = Questions.IndexOf(_currentQuestion);
                }
            }
            else
            {
                hasNextQuestion = false;
            }
        }

        return SurveyResult.EndOfSurvey();
    }

    public void SetQuota(SurveyQuota surveyQuota) => SurveyQuota = surveyQuota;
}