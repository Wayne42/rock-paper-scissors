using Spectre.Console;

namespace SSP
{
    class GameRenderer
    {
        private static Random rnd = new Random();
        private static readonly String[] _winRoundMessages = { "Keep it up! ", "Well done! ", "Astonishing! ", "Incredible outplay! ", "Very impressive! " };
        private static readonly String[] _loseRoundMessages = { "Keep fighting! ", "Sometimes you lose some. :/ ", "Never surrender! ", "Stay determined. " };
        private static readonly String[] _tieRoundMessages = { "WOW. ", "What are the odds? ", "Astonishing! ", "Stay determined. ", "Keep fighting! " };

        public static void PrintIntro()
        {
            AnsiConsole.Write(
            new FigletText("Rock")
                .LeftJustified()
                .Color(Color.Red));

            AnsiConsole.Write(
            new FigletText("Paper")
                .Centered()
                .Color(Color.Green));

            AnsiConsole.Write(
            new FigletText("Scissors")
                .RightJustified()
                .Color(Color.Blue));
        }

        public static void PrintRound(Move playerOne, Move playerTwo)
        {

            // AnsiConsole.MarkupLine($"{GetMoveSymbol(playerOne)} vs. {GetMoveSymbol(playerTwo)}");
            Table table = new Table();

            table.AddColumns("Your Move", "Enemy Move");

            table.AddRow(GetMoveSymbol(playerOne), GetMoveSymbol(playerTwo));

            AnsiConsole.Write(table);
        }

        public static void PrintPlayerWonRound()
        {
            String msg = _winRoundMessages[rnd.Next(_winRoundMessages.Count())];
            // AnsiConsole.MarkupLine($"[green]You won the round! {msg}[/] ");
            _AnimateText("green", $"You won the round! {msg}");
        }

        public static void PrintComputerWonRound()
        {
            String msg = _loseRoundMessages[rnd.Next(_loseRoundMessages.Count())];
            // AnsiConsole.MarkupLine($"[red]You lost the round. {msg}[/] ");
            _AnimateText("red", $"You lost the round! {msg}");
        }

        public static void PrintNooneWonRound()
        {
            String msg = _tieRoundMessages[rnd.Next(_tieRoundMessages.Count())];
            // AnsiConsole.MarkupLine($"[blue]Tie! {msg}Try again![/] ");
            _AnimateText("blue", $"Tie! {msg}Try again!");
        }


        public static void PrintPlayerWonGame()
        {
            // AnsiConsole.MarkupLine($"[green]You won the Game!!![/] ");
            _AnimateText("green", $"You won the Game!!!");
        }

        public static void PrintComputerWonGame()
        {
            // AnsiConsole.MarkupLine($"[red]You lost the Game :/ ...[/] ");
            _AnimateText("red", $"You lost the Game :/ ...");
        }

        public static void PrintLineBreak()
        {
            AnsiConsole.MarkupLine("\n[gray] - - - - - [/]\n");
        }

        public static void PrintStats(int playerPoints, int computerPoints, int pointsMax)
        {
            AnsiConsole.MarkupLine($"[gray]You ({playerPoints}), Computer ({computerPoints}), First to reach ({pointsMax}) points. [/]");
        }

        public static void PrintComputerThinking()
        {
            _AnimateText("gray", "Computer is thinking...");
            Thread.Sleep(600);
            _AnimateText("gray", "Rock...");
            Thread.Sleep(250);
            _AnimateText("gray", "Paper...");
            Thread.Sleep(250);
            _AnimateText("gray", "Scissors...");
            Thread.Sleep(250);
        }

        private static void _PrintAnimatedDotsNL()
        {
            _PrintAnimatedDots(3);
            AnsiConsole.WriteLine();
        }
        private static void _PrintAnimatedDots(int count)
        {
            for (var i = 0; i < count; i++)
            {
                AnsiConsole.Markup($"[gray].[/]");
                Thread.Sleep(100);
            }
        }

        private static void _AnimateText(String color, String text)
        {
            bool isColorValid = false;
            String[] validColors = { "gray", "blue", "red", "green" };
            foreach (string item in validColors) // valid colors
            {
                if (item.Contains(color))
                {
                    isColorValid = true;
                    break;
                }
            }
            if (!isColorValid) { throw new Exception("Invalid Color passed to AnimateText Function. "); }

            for (var i = 0; i < text.Length; i++)
            {
                AnsiConsole.Markup($"[{color}]{text[i]}[/]");
                Thread.Sleep(14);
            }
            AnsiConsole.WriteLine();
        }

        private static String GetMoveSymbol(Move move)
        {
            switch (move)
            {
                case Move.Rock:
                    return "Rock";
                case Move.Paper:
                    return "Paper";
                case Move.Scissors:
                    return "Scissors";
                default: // should never happen
                    return "0";
            }
        }
    }
}
