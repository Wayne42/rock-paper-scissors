using Spectre.Console;

namespace SSP
{
    class GameControl
    {
        public static GameMode.Mode ChooseMode()
        {
            var mode = AnsiConsole.Prompt(
                new SelectionPrompt<String>()
                    .Title("Which [green]Mode[/] do you want to play? ?")
                    .PageSize(5)
                    .MoreChoicesText("[grey](Move up and down to reveal more Options)[/]")
                    .AddChoices(GameMode.ModeNames)
                    );

            AnsiConsole.MarkupLine($"You chose [green]{mode}[/]. Let's play! ");

            return GameMode.GameModeFromString(mode);
        }

        public static Move ChooseTurn()
        {
            var move = AnsiConsole.Prompt(
                new SelectionPrompt<String>()
                    .Title("Choose your Move!")
                    .PageSize(3)
                    .MoreChoicesText("[grey](Move up and down to reveal more Options)[/]")
                    .AddChoices("Rock", "Paper", "Scissors")
                    );

            return (Move)Enum.Parse(typeof(Move), move);
        }

        public static bool ChoosePlayAgain()
        {
            var decision = AnsiConsole.Prompt(
                new SelectionPrompt<String>()
                    .Title("Play again?")
                    .PageSize(3)
                    .MoreChoicesText("[grey](Move up and down to reveal more Options)[/]")
                    .AddChoices("yes", "no")
                    );

            return decision == "yes";
        }

        public static void WaitForAnyKey()
        {
            AnsiConsole.MarkupLine($"[gray]press any key to continue. [/]");
            Console.ReadKey();
        }
    }
}
