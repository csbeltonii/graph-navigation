namespace GraphSurvey;

public class SimpleGraphExample
{
    public void Execute()
    {
        var q1 = SurveyObjectMetaData.Create("Q1", "question");
        var q2 = SurveyObjectMetaData.Create("Q2", "question");
        var q3 = SurveyObjectMetaData.Create("Q3", "question");

        var graph = new Graph<SurveyObjectMetaData>();

        graph.Add(q1);
        graph.Add(q2);
        graph.Add(q3);

        Console.WriteLine("Iterating graph nodes");
        graph.Nodes.ForEach(node => Console.WriteLine($"{node.GraphData.Type} - {node.GraphData.Name}, Index : {node.Index}"));
    }
}