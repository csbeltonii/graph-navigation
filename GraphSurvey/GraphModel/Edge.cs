using GraphSurvey.SurveyModel;

namespace GraphSurvey.GraphModel;

/// <summary>
/// Represents a connection between two nodes.
/// TODO: Intended to be saved as a Cosmos document
/// </summary>
/// <typeparam name="TSurveyObjectData"></typeparam>
public record Edge<TSurveyObjectData>
    where TSurveyObjectData : ISurveyObjectMetaData, new()
{
    public required Node<TSurveyObjectData> From { get; set; }

    public required Node<TSurveyObjectData> To { get; set; }
    public required Condition? Condition { get; set; }
}