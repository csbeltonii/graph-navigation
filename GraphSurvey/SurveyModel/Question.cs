using GraphSurvey.GraphModel;

namespace GraphSurvey.SurveyModel;

/// <summary>
/// An implementation of a Question Survey object.
/// Provides metadata to display to the user and navigation conditions for the graph
/// based on the choices.
/// </summary>
public class Question : ISurveyObjectMetaData
{
    public Question() { }
    public Question(string text, string name) => (Name, Text) = (name, text);

    public ICollection<Choice> Choices { get; set; } = [];
    public string Text { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Type => "question";
    public IReadOnlyList<NavigationCondition> NavigationConditions { get; set; } = [];

    public void Display()
    {
        var choiceIndex = 1;
        Console.WriteLine(Text);

        foreach (var choice in Choices)
        {
            Console.WriteLine($"{choiceIndex}: {choice} ({choice.Value})");
            choiceIndex++;
        }
    }
}