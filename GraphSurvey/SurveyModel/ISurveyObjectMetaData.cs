using GraphSurvey.GraphModel;

namespace GraphSurvey.SurveyModel;

public interface ISurveyObjectMetaData
{
    public string Name { get; }
    public string Type { get; }
    public IReadOnlyList<NavigationCondition> NavigationConditions { get; }
}