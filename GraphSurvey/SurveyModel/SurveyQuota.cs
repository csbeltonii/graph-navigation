using GraphSurvey.GraphModel;

namespace GraphSurvey.SurveyModel;

public class SurveyQuota(string name, IReadOnlyList<QuotaCell> quotaCells)
{
    public string Name { get; init; } = name;
    public IReadOnlyList<QuotaCell> QuotaCells { get; set; } = quotaCells;
}

public class QuotaMarker(string name) : ISurveyObjectMetaData
{
    public string Name { get; } = name;
    public string Type => "quota";
    public IReadOnlyList<NavigationCondition> NavigationConditions { get; } = [];
}