using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSP
{
    public static class GameManager
    {
        public static void StartRockPaperScissors() {
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

                    GameRenderer.PrintGameState(currentState);

                    GameControl.WaitForAnyKey();

                    int[] stats = GameLogic.GetStats();
                    GameRenderer.PrintStats(stats[0], stats[1], stats[2]);
                    GameRenderer.PrintLineBreak();

                } while (currentState == GameState.PlayerWonRound || currentState == GameState.ComputerWonRound || currentState == GameState.NooneWonRound);

                GameRenderer.PrintGameState(currentState);
                GameRenderer.PrintLineBreak();

                playAgain = GameControl.ChoosePlayAgain();
                if (playAgain)
                {
                    gameMode = GameControl.ChooseMode();
                    GameLogic.InitNewGame(gameMode);
                }
            } while (playAgain);

        }
    }
}
