using GraphSurvey.GraphModel;
using GraphSurvey.SurveyModel;
using static System.Console;

namespace GraphSurvey;

/// <summary>
/// A simple survey based on beginning a Pokémon Journey.
/// Demonstrates graph navigation using choices to navigate linearly, dynamically,
/// and evaluate a quota.
/// </summary>
public static class ThePokemonSurvey
{
    /// <summary>
    /// Generates seven questions pertaining to starting a Pokémon adventure.
    /// </summary>
    /// <returns></returns>
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
                new NavigationCondition("R1", "QUOTA1"),
                new NavigationCondition("R2", "QUOTA2")
            ]
        };

        var q2 = new Question("What is your starter Pokemon?", "Q_STARTER")
        {
            Choices =
            [
                new Choice("R1", 1, "Bulbasaur"),
                new Choice("R2", 2, "Charmander"),
                new Choice("R3", 3, "Squirtle"),
                new Choice("R4", 4, "Previous")
            ],
            NavigationConditions =
            [
                new NavigationCondition("R1","Q_BEGIN"),
                new NavigationCondition("R2", "Q_BEGIN"),
                new NavigationCondition("R3", "Q_BEGIN"),
                new NavigationCondition("R4", "Q_GENDER")
            ]
        };

        var q3 = new Question("Let's begin your Pokemon adventure", "Q_BEGIN")
        {
            Choices =
            [
                new Choice("R1", 1, "Continue"),
                new Choice("R2", 2, "Previous")
            ],
            NavigationConditions =
            [
                new NavigationCondition("R2", "Q_STARTER"),
                new NavigationCondition("R1", "Q_REGION")
            ]
        };

        var q4 = new Question("What region would you like to start in?", "Q_REGION")
        {
            Choices =
            [
                new Choice("R1", 1, "Kanto"),
                new Choice("R2", 2, "Johto"),
                new Choice("R3", 3, "Hoenn"),
                new Choice("R4", 4, "Previous")
            ],
            NavigationConditions =
            [
                new NavigationCondition("R1", "Q_KANTO"),
                new NavigationCondition("R2", "Q_JOHTO"),
                new NavigationCondition("R3", "Q_HOENN"),
                new NavigationCondition("R4", "Q_BEGIN")
            ]
        };

        var q5 = new Question("There is nothing to do in Kanto", "Q_KANTO")
        {
            Choices = 
            [
                new Choice("R1", 1, "Complete", completeAfter: true),
                new Choice("R2", 2, "Previous")
            ],
            NavigationConditions =
            [
                new NavigationCondition("R2", "Q_REGION")
            ]
        };

        var q6 = new Question("There is nothing to do in Johto", "Q_JOHTO")
        {
            Choices =
            [
                new Choice("R1", 1, "Complete", completeAfter: true),
                new Choice("R2", 2, "Previous")
            ],
            NavigationConditions =
            [
                new NavigationCondition("R2", "Q_REGION")
            ]
        };

        var q7 = new Question("There is nothing to do in Hoenn", "Q_HOENN")
        {
            Choices =
            [
                new Choice("R1", 1, "Complete", completeAfter: true),
                new Choice("R2", 2, "Previous")
            ],
            NavigationConditions =
            [
                new NavigationCondition("R2", "Q_REGION")
            ]
        };
        
        return
        [
            q1,
            q2,
            q3, 
            q4,
            q5,
            q6,
            q7
        ];
    }

    /// <summary>
    /// Generates the survey graph based on the provided survey object metadata
    /// </summary>
    /// <param name="surveyObjectMetaData"></param>
    /// <param name="delayBetweenActions"></param>
    /// <returns></returns>
    public static Graph<SurveyObjectMetaData> GeneratePokemonSurveyGraph(IEnumerable<SurveyObjectMetaData> surveyObjectMetaData, bool delayBetweenActions = false)
    {
        var graph = new Graph<SurveyObjectMetaData>();

        WriteLine("==========================");
        WriteLine("==========================");
        WriteLine("==========================");
        WriteLine();
        WriteLine("Adding nodes to graph.");
        WriteLine();

        var nodes = surveyObjectMetaData
                    .Select(graph.Add)
                    .ToList();

        WriteLine("==========================");
        WriteLine("==========================");
        WriteLine("==========================");

        WriteLine("Generating nodes neighbor lists.");
        WriteLine();

        foreach (var node in nodes)
        {
            var neighbors = new HashSet<Node<SurveyObjectMetaData>>();
            var nodeName = node.GraphData.Name;

            WriteLine($"Adding neighbors for node: {nodeName}");

            if (node.NavigationConditions.Count > 0)
            {
                var dynamicNavigationNeighbors = nodes.Where(
                    n => node
                         .NavigationConditions
                         .Select(nc => nc.TargetNode)
                         .Contains(n.GraphData.Name)
                );

                foreach (var dynamicNavigationNeighbor in dynamicNavigationNeighbors)
                {
                    neighbors.Add(dynamicNavigationNeighbor);
                }
            }
            else 
            {
                if (nodes.FirstOrDefault(
                        neighbor => neighbor.Index == node.Index + 1
                    ) is { } nextNode)
                {
                    neighbors.Add(nextNode);
                }
            }

            node.Neighbors = neighbors.ToList();

            WriteLine($"\t{nodeName} can navigate to {string.Join(", ", node.Neighbors.Select(_ => _.GraphData.Name))}");
            WriteLine();
        }

        WriteLine("==========================");
        WriteLine("==========================");
        WriteLine("==========================");
        WriteLine();
        WriteLine("Generating edges...");
        WriteLine();

        var edges = graph.GetEdges();

        foreach (var edge in edges)
        {
            WriteLine($"On Edge {edge.Condition?.Value}, {edge.From.GraphData.Name} connects to {edge.To.GraphData.Name}.");
            WriteLine();
        }

        WriteLine("==========================");
        WriteLine("==========================");
        WriteLine("==========================");

        return graph;
    }

    public static SurveyQuota GenerateQuota() => new(
        "QUOTA1",
        [
            new QuotaCell("Q_GENDER.R1", 1),
            new QuotaCell("Q_GENDER.R2", 1),
        ]
    );
}