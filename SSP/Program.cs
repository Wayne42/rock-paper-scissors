using SSP;

GameRenderer.PrintIntro();

var gameMode = GameControl.ChooseMode();

GameRenderer.PrintLineBreak();
GameRenderer.PrintLineBreak();

GameLogic.InitNewGame(gameMode);

bool playAgain = false;
do
{
    GameState CurrentState;
    do
    {
        var Move = GameControl.ChooseTurn();
        CurrentState = GameLogic.NextRound(Move);

        GameRenderer.PrintComputerThinking();
        GameRenderer.PrintRound(Move, GameLogic.CurrentComputerMove);

        if (CurrentState == GameState.PlayerWonRound || CurrentState == GameState.PlayerWonGame)
        {
            GameRenderer.PrintPlayerWonRound();
        }
        else if (CurrentState == GameState.ComputerWonRound || CurrentState == GameState.ComputerWonGame)
        {
            GameRenderer.PrintComputerWonRound();
        }
        else if (CurrentState == GameState.NooneWonRound)
        {
            GameRenderer.PrintNooneWonRound();
        }

        GameControl.WaitForAnyKey();

        int[] stats = GameLogic.GetStats();
        GameRenderer.PrintStats(stats[0], stats[1], stats[2]);
        GameRenderer.PrintLineBreak();

    } while (CurrentState == GameState.PlayerWonRound || CurrentState == GameState.ComputerWonRound || CurrentState == GameState.NooneWonRound);

    if (CurrentState == GameState.PlayerWonGame)
    {
        GameRenderer.PrintPlayerWonGame();
    }
    else
    {
        GameRenderer.PrintComputerWonGame();
    }
    GameRenderer.PrintLineBreak();

    playAgain = GameControl.ChoosePlayAgain();
    if (playAgain)
    {
        gameMode = GameControl.ChooseMode();
        GameLogic.InitNewGame(gameMode);
    }
} while (playAgain);

