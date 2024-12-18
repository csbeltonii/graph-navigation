using GraphSurvey.SurveyModel;

namespace GraphSurvey.GraphModel;

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