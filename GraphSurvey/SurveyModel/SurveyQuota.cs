namespace GraphSurvey.SurveyModel;

/// <summary>
/// An implementation of a Quota table survey object.
/// Maintains a list of cells to evaluate quota counts.
/// </summary>
/// <param name="name"></param>
/// <param name="quotaCells"></param>
public class SurveyQuota(string name, IReadOnlyList<QuotaCell> quotaCells)
{
    public string Name { get; init; } = name;
    public IReadOnlyList<QuotaCell> QuotaCells { get; set; } = quotaCells;
}