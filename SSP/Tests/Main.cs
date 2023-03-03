using Xunit;

namespace SSP.Tests
{
    public class Main
    {
        [Theory]
        [InlineData(Move.Scissors, Move.Rock)]
        [InlineData(Move.Rock, Move.Paper)]
        [InlineData(Move.Paper, Move.Scissors)]
        private void TestRival_Always_Tie(Move a, Move b)
        {
            // Arrange
            GameLogic.InitNewGame(GameMode.Mode.Rival);

            // Act
            var state = GameLogic.NextRound(a);
            state = GameLogic.NextRound(b);

            // Assert
            Assert.Equal(GameState.NooneWonRound, state);
        }

        [Theory]
        [InlineData(Move.Scissors, Move.Scissors)]
        [InlineData(Move.Rock, Move.Rock)]
        [InlineData(Move.Paper, Move.Paper)]
        private void TestRival_Always_Lose(Move a, Move b)
        {
            // Arrange
            GameLogic.InitNewGame(GameMode.Mode.Rival);

            // Act
            var state = GameLogic.NextRound(a);
            state = GameLogic.NextRound(b);

            // Assert
            Assert.Equal(GameState.ComputerWonRound, state);
        }

        [Theory]
        [InlineData(Move.Scissors, Move.Paper)]
        [InlineData(Move.Rock, Move.Scissors)]
        [InlineData(Move.Paper, Move.Rock)]
        private void TestRival_Always_Win(Move a, Move b)
        {
            // Arrange
            GameLogic.InitNewGame(GameMode.Mode.Rival);

            // Act
            var state = GameLogic.NextRound(a);
            state = GameLogic.NextRound(b);

            // Assert
            Assert.Equal(GameState.PlayerWonRound, state);
        }

        [Theory]
        [InlineData(Move.Scissors, Move.Paper, Move.Rock, Move.Scissors)]
        [InlineData(Move.Rock, Move.Scissors, Move.Paper, Move.Rock)]
        [InlineData(Move.Paper, Move.Rock, Move.Scissors, Move.Paper)]
        private void TestRival_Win_Game(Move a, Move b, Move c, Move d)
        {
            // Arrange
            GameLogic.InitNewGame(GameMode.Mode.Rival);

            // Act
            var state = GameLogic.NextRound(a);
            state = GameLogic.NextRound(b);
            state = GameLogic.NextRound(c);

            // Assert (if first move won round by chance, it is possible to already win the game at this point)
            if (GameState.PlayerWonGame == state)
            {
                Assert.Equal(GameState.PlayerWonGame, state);
            }
            else
            {
                // Act
                state = GameLogic.NextRound(d);

                // Assert
                Assert.Equal(GameState.PlayerWonGame, state);
            }
        }

    }
}
