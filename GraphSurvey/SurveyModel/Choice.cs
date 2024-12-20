namespace GraphSurvey.SurveyModel;

/// <summary>
/// Represents a choice on a given question.
/// Has the ability to terminate or complete a survey after answering.
/// </summary>
/// <param name="name"></param>
/// <param name="value"></param>
/// <param name="text"></param>
/// <param name="isTerminate"></param>
/// <param name="completeAfter"></param>
public class Choice(string name, int value, string text, bool isTerminate = false, bool completeAfter = false)
{
    public string Name { get; set; } = name;
    public int Value { get; set; } = value;
    public string Text { get; set; } = text;
    public bool IsTerminate { get; set; } = isTerminate;
    public bool CompleteAfter { get; set; } = completeAfter;
    public string DslValue(string questionName) => $"{questionName}.{Name}";
    public override string ToString() => Text;
}