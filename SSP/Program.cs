using SSP;

GameRenderer.PrintIntro();

var gameMode = GameControl.ChooseMode();

GameRenderer.PrintLineBreak();
GameRenderer.PrintLineBreak();

GameLogic.InitNewGame(gameMode);

bool playAgain = false;
do
{
    GameState currentState;
    do
    {
        var move = GameControl.ChooseTurn();
        currentState = GameLogic.NextRound(move);

        GameRenderer.PrintComputerThinking();
        GameRenderer.PrintRound(move, GameLogic.CurrentComputerMove);


        if (currentState == GameState.PlayerWonRound || currentState == GameState.PlayerWonGame)
        {
            GameRenderer.PrintPlayerWonRound();
        }
        else if (currentState == GameState.ComputerWonRound || currentState == GameState.ComputerWonGame)
        {
            GameRenderer.PrintComputerWonRound();
        }
        else if (currentState == GameState.NooneWonRound)
        {
            GameRenderer.PrintNooneWonRound();
        }

        GameControl.WaitForAnyKey();

        int[] stats = GameLogic.GetStats();
        GameRenderer.PrintStats(stats[0], stats[1], stats[2]);
        GameRenderer.PrintLineBreak();

    } while (currentState == GameState.PlayerWonRound || currentState == GameState.ComputerWonRound || currentState == GameState.NooneWonRound);

    if (currentState == GameState.PlayerWonGame)
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

