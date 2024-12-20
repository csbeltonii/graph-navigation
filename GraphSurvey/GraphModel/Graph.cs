using GraphSurvey.SurveyModel;

namespace GraphSurvey.GraphModel;

/// <summary>
/// Generic graph for survey object metadata that maintains a list of nodes.
/// TODO: Should this graph utilize survey objects or pages?
/// TODO: How to handle blocks?
/// TODO: How to handle pages with multiple questions?
/// </summary>
/// <typeparam name="TSurveyObjectData"></typeparam>
public class Graph<TSurveyObjectData>
    where TSurveyObjectData : ISurveyObjectMetaData, new()
{
    public List<Node<TSurveyObjectData>> Nodes { get; set; } = [];
    public int NodeCount => Nodes.Count;

    /// <summary>
    /// Generates edges using the neighbors (adjacency matrix) of the nodes.
    /// </summary>
    /// <returns></returns>
    public IReadOnlyList<Edge<TSurveyObjectData>> GetEdges()
        => Nodes
           .SelectMany(node =>
                           node.Neighbors.Select(
                               (to, index) => new Edge<TSurveyObjectData>
                               {
                                   From = node,
                                   To = to,
                                   Condition = node.NavigationConditions.Count > 0 
                                       ?  node.NavigationConditions[index]
                                       : new NavigationCondition("Any", "")
                               }
                           )
           )
           .ToList();

    /// <summary>
    /// Adds the data 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public Node<TSurveyObjectData> Add(TSurveyObjectData value)
    {
        var node = new Node<TSurveyObjectData>
        {
            Index = NodeCount,
            GraphData = value,
            NavigationConditions = value.NavigationConditions
        };

        Nodes.Add(node);

        return node;
    }
}