namespace GraphSurvey.SurveyModel;

public class QuotaCell(string condition, int target)
{
    public string Condition { get; set; } = condition;
    public int Count { get; set; }
    public int Target { get; set; } = target;
    public bool Filled => Count == Target;
}