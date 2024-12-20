using GraphSurvey.SurveyModel;

namespace GraphSurvey.GraphModel;

/// <summary>
/// Represents a node on the graph.
/// TODO: Store in Cosmos for point-read operations when node data is required.
/// </summary>
/// <typeparam name="TSurveyObjectData"></typeparam>
public record Node<TSurveyObjectData>
    where TSurveyObjectData : ISurveyObjectMetaData, new()
{
    public int Index { get; set; } = -1;

    public TSurveyObjectData GraphData { get; set; } = new();

    public IList<Node<TSurveyObjectData>> Neighbors { get; set; } = [];

    public IReadOnlyList<NavigationCondition> NavigationConditions { get; set; } = [];
}