using GraphSurvey.GraphModel;

namespace GraphSurvey.SurveyModel;

/// <summary>
/// An interface that provides metadata about survey objects and their navigation conditions
/// to be evaluated for graph navigation.
/// </summary>
public interface ISurveyObjectMetaData
{
    public string Name { get; }
    public string Type { get; }
    public IReadOnlyList<NavigationCondition> NavigationConditions { get; }
}