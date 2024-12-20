using GraphSurvey.GraphModel;

namespace GraphSurvey.SurveyModel;

/// <summary>
/// Metadata for survey objects used in graph traversal
/// </summary>
public class SurveyObjectMetaData : ISurveyObjectMetaData
{
    public static SurveyObjectMetaData Create(string name, string type) => new()
    {
        Name = name,
        Type = type
    };

    public static SurveyObjectMetaData Create(ISurveyObjectMetaData surveyObject) => new()
    {
        Name = surveyObject.Name,
        Type = surveyObject.Type,
        NavigationConditions = surveyObject.NavigationConditions
    };

    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;

    public IReadOnlyList<NavigationCondition> NavigationConditions { get; set; } = [];
}