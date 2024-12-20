using GraphSurvey.GraphModel;

namespace GraphSurvey.SurveyModel;

/// <summary>
/// An implementation of a Quota Marker survey object.
/// Indicates the position(s) to evaluate the provided quota name.
/// </summary>
/// <param name="name"></param>
public class QuotaMarker(string name) : ISurveyObjectMetaData
{
    public string Name { get; } = name;
    public string Type => "quota";
    public IReadOnlyList<NavigationCondition> NavigationConditions { get; } = [];
}