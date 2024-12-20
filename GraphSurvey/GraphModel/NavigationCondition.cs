namespace GraphSurvey.GraphModel;

/// <summary>
/// Simple condition object.
/// </summary>
/// <param name="Value"></param>
public record Condition(string? Value);

/// <summary>
/// Expands on conditions by adding a node to journey to if the condition is fulfilled.
/// </summary>
/// <param name="Value"></param>
/// <param name="TargetNode"></param>
public record NavigationCondition(string? Value, string TargetNode)
    : Condition(Value);