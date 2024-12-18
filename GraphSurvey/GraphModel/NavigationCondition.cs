namespace GraphSurvey.GraphModel;

public record Condition(string Value);

public record NavigationCondition(string Value, string TargetNode)
    : Condition(Value);