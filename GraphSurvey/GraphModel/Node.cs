using GraphSurvey.SurveyModel;

namespace GraphSurvey.GraphModel;

public record Node<TSurveyObjectData>
    where TSurveyObjectData : ISurveyObjectMetaData, new()
{
    public int Index { get; set; } = -1;

    public TSurveyObjectData GraphData { get; set; } = new();

    public IList<Node<TSurveyObjectData>> Neighbors { get; set; } = [];

    public IReadOnlyList<NavigationCondition> NavigationConditions { get; set; } = [];
}