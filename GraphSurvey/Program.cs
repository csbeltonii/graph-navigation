using GraphSurvey;
using static System.Console;

WriteLine("Use 0 to navigate backwards.");

while (true)
{
    var questions = ThePokemonSurvey.GeneratePokemonQuestions();

    var pokemonBeginnerSurvey = new Survey(
        questions,
        ThePokemonSurvey.GeneratePokemonSurveyGraph(questions.Select(SurveyObjectMetaData.Create)));

    pokemonBeginnerSurvey.TakeSurvey();

    WriteLine();
    Write("Take survey again? Enter Y or N: ");

    if (ReadKey().Key is ConsoleKey.N)
    {
        WriteLine();
        break;
    }
}

WriteLine("Thanks for taking this survey.");