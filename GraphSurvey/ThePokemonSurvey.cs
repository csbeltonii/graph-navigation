namespace GraphSurvey;

public static class ThePokemonSurvey
{
    private static Dictionary<string, int> Quota = [];

    public static List<Question> GeneratePokemonQuestions()
    {
        var q1 = new Question("Welcome to the world of Pokemon. Are you a boy or a girl?", "Q_GENDER")
        {
            Choices =
            [
                new Choice("R1", 1, "Boy"),
                new Choice("R2", 2, "Girl"),
                new Choice("R3", 3, "Prefer not to say", isTerminate: true)
            ],
            NavigationConditions =
            [
                new NavigationCondition("R3", "Q_END")
            ]
        };

        var q2 = new Question("What is your starter Pokemon?", "Q_STARTER")
        {
            Choices =
            [
                new Choice("R1", 1, "Bulbasaur"),
                new Choice("R2", 2, "Charmander"),
                new Choice("R3", 3, "Squirtle")
            ]
        };

        var q3 = new Question("Let's begin your Pokemon adventure", "Q_END");


        return
        [
            q1,
            q2,
            q3
        ];
    }

    public static Graph<SurveyObjectMetaData> GeneratePokemonSurveyGraph(IEnumerable<SurveyObjectMetaData> surveyObjectMetaData)
    {
        var graph = new Graph<SurveyObjectMetaData>();

        var nodes = surveyObjectMetaData
                    .Select(graph.Add)
                    .ToList();

        foreach (var node in nodes)
        {
            var neighbors = new List<Node<SurveyObjectMetaData>>();

            if (node.NavigationConditions.Count > 0)
            {
                neighbors.AddRange(
                    nodes.Where(n => node
                                     .NavigationConditions
                                     .Select(nc => nc.TargetNode)
                                     .Contains(n.GraphData.Name))
                );
            }

            neighbors.AddRange(nodes
                               .Where(n => n.Index ==
                                           node.Index++ ||
                                           n.Index == node.Index--
                               )
                               .ToList()
            );

            node.Neighbors = neighbors;
        }

        return graph;
    }
}