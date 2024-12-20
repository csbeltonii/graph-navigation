using GraphSurvey;
using GraphSurvey.SurveyModel;
using static System.Console;

var questions = ThePokemonSurvey.GeneratePokemonQuestions();
var quotaMarker = new QuotaMarker("QUOTA1");

var questionMetaData = questions.Select(SurveyObjectMetaData.Create).ToList();
questionMetaData.Insert(1, SurveyObjectMetaData.Create(quotaMarker));

var pokemonBeginnerSurvey = new Survey(
    questions,
    ThePokemonSurvey.GeneratePokemonSurveyGraph(questionMetaData));

pokemonBeginnerSurvey.SetQuota(ThePokemonSurvey.GenerateQuota());

while (true)
{
    WriteLine();

    var surveyResult = pokemonBeginnerSurvey.TakeSurveyV2();

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