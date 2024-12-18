namespace GraphSurvey.SurveyModel;

public class Choice(string name, int value, string text, bool isTerminate = false)
{
    public string Name { get; set; } = name;
    public int Value { get; set; } = value;
    public string Text { get; set; } = text;
    public bool IsTerminate { get; set; } = isTerminate;
    public string DslValue(string questionName) => $"{questionName}.{Name}";
    public override string ToString() => Text;
}