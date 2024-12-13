namespace GraphSurvey;

public record Node<TSurveyObjectData>
    where TSurveyObjectData : ISurveyObjectMetaData, new()
{
    public int Index { get; set; } = -1;

    public TSurveyObjectData GraphData { get; set; } = new();

    public IReadOnlyList<Node<TSurveyObjectData>> Neighbors { get; set; } = [];

    public IReadOnlyList<NavigationCondition> NavigationConditions { get; set; } = [];
}

public record Edge<TSurveyObjectData>
    where TSurveyObjectData : ISurveyObjectMetaData, new()
{
    public required Node<TSurveyObjectData> From { get; set; }

    public required Node<TSurveyObjectData> To { get; set; }
    public required Condition Condition { get; set; } 
}

public class Graph<TSurveyObjectData>
    where TSurveyObjectData : ISurveyObjectMetaData, new()
{
    public List<Node<TSurveyObjectData>> Nodes { get; set; } = [];
    public int NodeCount => Nodes.Count;

    public IReadOnlyList<Edge<TSurveyObjectData>> GetEdges() 
        => Nodes
           .SelectMany(node => 
                       node.Neighbors.Select(
                           (to, index) => new Edge<TSurveyObjectData>
                           {
                               From = node,
                               To = to,
                               Condition = node.NavigationConditions[index]
                           }
                       )
           )
           .ToList();

    public Node<TSurveyObjectData> Add(TSurveyObjectData value)
    {
        var node = new Node<TSurveyObjectData>
        {
            Index = NodeCount + 1,
            GraphData = value,
            NavigationConditions = value.NavigationConditions
        };

        Nodes.Add(node);

        return node;
    }
}