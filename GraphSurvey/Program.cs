using GraphSurvey;
using GraphSurvey.SurveyModel;
using static System.Console;

WriteLine("Use 0 to navigate backwards.");
var questions = ThePokemonSurvey.GeneratePokemonQuestions();

var pokemonBeginnerSurvey = new Survey(
    questions,
    ThePokemonSurvey.GeneratePokemonSurveyGraph(questions.Select(SurveyObjectMetaData.Create)));

pokemonBeginnerSurvey.SetQuota(ThePokemonSurvey.GenerateQuota());

while (true)
{
    WriteLine();

    var surveyResult = pokemonBeginnerSurvey.TakeSurvey();

    WriteLine($"{surveyResult.Message} ({surveyResult.CompletionType})");

    WriteLine();
    Write("Take survey again? Enter Y or N: ");
    WriteLine();

    if (ReadKey().Key is ConsoleKey.N)
    {
        WriteLine();
        break;
    }
}

WriteLine("Thanks for taking this survey.");