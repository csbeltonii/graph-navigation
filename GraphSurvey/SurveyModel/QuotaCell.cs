namespace GraphSurvey.SurveyModel;

/// <summary>
/// An implementation of a quota cell.
/// Tracks number of respondents that have answered the given condition.
/// </summary>
/// <param name="condition"></param>
/// <param name="target"></param>
public class QuotaCell(string condition, int target)
{
    public string Condition { get; set; } = condition;
    public int Count { get; set; }
    public int Target { get; set; } = target;
    public bool Filled => Count == Target;
}