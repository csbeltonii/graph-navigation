namespace GraphSurvey.SurveyModel;

public record SurveyResult
{
    private SurveyResult(string message, CompletionType completionType) =>
        (Message, CompletionType) = (message, completionType);

    public string Message { get; init; }
    public CompletionType CompletionType { get; init; }

    public static SurveyResult EndOfSurvey() => new("Respondent reached the end of survey.", CompletionType.EndOfSurvey);
    public static SurveyResult Terminated() => new("Respondent was terminated early.", CompletionType.Terminated);
    public static SurveyResult OverQuota() => new("Respondent was terminated due to quota.", CompletionType.OverQuota);
    public static SurveyResult Continue() => new(string.Empty, CompletionType.None);
}