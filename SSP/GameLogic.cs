namespace SSP
{
    enum GameState
    {
        PlayerWonRound,
        ComputerWonRound,
        NooneWonRound,
        PlayerWonGame,
        ComputerWonGame
    }
    class GameLogic
    {
        public static Move CurrentComputerMove;

        private static Stack<Move> _playerMoves = new();
        private static Stack<Move> _computerMoves = new();
        private static GameMode.Mode _mode;

        private static int _currentRound = 0;
        private static int _playerPoints = 0;
        private static int _computerPoints = 0;

        private static readonly int _pointsToWinGame = 3;

        private static bool _computerLostLastRound = false;

        public static void InitNewGame(GameMode.Mode mode)
        {
            _playerMoves = new();
            _computerMoves = new();
            _mode = mode;
            _currentRound = 0;
            _computerLostLastRound = false;
            _playerPoints = 0;
            _computerPoints = 0;
        }

        public static GameState NextRound(Move playerMove)
        {
            Move computerMove = GetNextComputerMove();
            _computerMoves.Push(computerMove);
            CurrentComputerMove = computerMove;

            _playerMoves.Push(playerMove);

            _currentRound++;

            _computerLostLastRound = false;

            GameState RoundState = EvalCurrentRound(playerMove, computerMove);
            if (RoundState == GameState.PlayerWonRound)
            {
                _computerLostLastRound = true;
                _playerPoints++;
            }
            else if (RoundState == GameState.ComputerWonRound)
            {
                _computerPoints++;
            }


            if (_playerPoints == _pointsToWinGame)
            {
                return GameState.PlayerWonGame;
            }
            else if (_computerPoints == _pointsToWinGame)
            {
                return GameState.ComputerWonGame;
            }

            if (_playerPoints > _pointsToWinGame || _computerPoints > _pointsToWinGame)
            {
                throw new Exception("New Game should have been initialized! ");
            }

            return RoundState;
        }

        private static GameState EvalCurrentRound(Move playerMove, Move computerMove)
        {
            if (playerMove == computerMove)
            {
                return GameState.NooneWonRound;
            }
            if (
                (playerMove == Move.Rock && computerMove == Move.Scissors) ||
                (playerMove == Move.Paper && computerMove == Move.Rock) ||
                (playerMove == Move.Scissors && computerMove == Move.Paper)
                )
            {
                return GameState.PlayerWonRound;
            }

            return GameState.ComputerWonRound;
        }

        private static Move GetNextComputerMove()
        {
            switch (_mode)
            {
                case GameMode.Mode.Normal:
                    return _NormalMove();
                case GameMode.Mode.Rival:
                    return _RivalMove();
                case GameMode.Mode.NCARS:
                    return _NCARSMove();
                default:
                    return _NormalMove();
            }
        }

        private static Random rnd = new();
        private static Move _NormalMove()
        {
            // creates a random number between 0 and 2;
            switch (rnd.Next(0, 3))
            {
                case 0:
                    return Move.Rock;
                case 1:
                    return Move.Paper;
                case 2:
                    return Move.Scissors;
                default: // should never happen
                    return Move.Rock;
            }
        }

        // Der Computer spielt immer das, was den Spieler in der letzten Runde geschlagen hätte
        private static Move _RivalMove()
        {
            if (_playerMoves.Count == 0)
            {
                return _NormalMove();
            }
            switch (_playerMoves.Peek())
            {
                case Move.Rock:
                    return Move.Paper;
                case Move.Paper:
                    return Move.Scissors;
                case Move.Scissors:
                    return Move.Rock;
                default:
                    // should never happen
                    return Move.Paper;
            }

        }

        // Der Computer wählt in der ersten Runde zufällig aus und behält diese Auswahl bei. Erst wenn der Computer verliert, wird die Auswahl (Stein, Schere, Papier) gewechselt.
        private static Move _NCARSMove()
        {
            if ((!_computerLostLastRound) && (_computerMoves.Count() > 0))
            {
                return _computerMoves.Peek();
            }

            return _NormalMove();
        }

        public static int[] GetStats()
        {
            return new int[] { _playerPoints, _computerPoints, _pointsToWinGame };
        }
    }
}
