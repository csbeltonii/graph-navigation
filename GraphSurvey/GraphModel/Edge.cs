using GraphSurvey.SurveyModel;

namespace GraphSurvey.GraphModel;

public record Edge<TSurveyObjectData>
    where TSurveyObjectData : ISurveyObjectMetaData, new()
{
    public required Node<TSurveyObjectData> From { get; set; }

    public required Node<TSurveyObjectData> To { get; set; }
    public required Condition? Condition { get; set; }
}