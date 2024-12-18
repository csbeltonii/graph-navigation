using GraphSurvey.GraphModel;

namespace GraphSurvey.SurveyModel;

public class SurveyQuota(IReadOnlyList<QuotaCell> quotaCells)
{ 
    public IReadOnlyList<QuotaCell> QuotaCells { get; set; } = quotaCells;
}