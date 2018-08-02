using System;
using System.Diagnostics.CodeAnalysis;
using Shuffle3.Model;

namespace Shuffle3.Logic
{
    public class GameFactory
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IUserInterface _userInterface;
        private readonly BoardFactory _boardFactory;

        public GameFactory(IUserInterface userInterface, BoardFactory boardFactory)
        {
            _userInterface = userInterface;
            _boardFactory = boardFactory;
        }
        /// <summary>
        /// Start the Game.
        /// </summary>
        [ExcludeFromCodeCoverage] //Cannot Test for User Input
        public void StartGame()
        {
            try
            {
                Board gameBoard = _boardFactory.Get();
                Logger.Info("New Game Board created");
                //Todo - Add a Player Class which has Player Name, and a Lives Counter
                //Todo - Request and Display Player Name
                _userInterface.RenderMessage("Welcome to Shuffle!");
                _userInterface.RenderMessage(
                    "Move your piece to the top of the board to win. Watch out for mines, hit two and its GAME OVER!");
                _userInterface.NewLine();
                bool success = gameBoard.DrawBoard();
                if (!success)
                {
                    throw new ApplicationException("Something went wrong while drawing the board");
                }

                _userInterface.NewLine();
                _userInterface.NewLine();
                _userInterface.RenderMessage("Ready Player One.");
                Logger.Info("Turns Started");
                TakeTurns(gameBoard);
            }
            catch (Exception exception)
            {
                Logger.Error(exception, "An Error Occured: {0}", exception.Message);
                _userInterface.RenderMessage($"An Error Occured: {exception}");
                _userInterface.RenderMessage("Press Any Key to Exit");
                _userInterface.GetUserInput();
            }
        }
        /// <summary>
        /// Take Turns until game completed.
        /// </summary>
        /// <param name="gameBoard"></param>
        [ExcludeFromCodeCoverage] //Cannot Test for User Input
        private void TakeTurns(Board gameBoard)
        {
            while (true)
            {
                string requestedMove = _userInterface.AskForMove();
                int move = _userInterface.ValidateMove(requestedMove);
                switch (move)
                {
                    case (int) Direction.Up:
                        _userInterface.ClearScreen();
                        _userInterface.RenderMessage("You moved up.");
                        gameBoard.MovePlayer(Direction.Up);
                        break;
                    case (int) Direction.Down:
                        _userInterface.ClearScreen();
                        _userInterface.RenderMessage("You moved down.");
                        gameBoard.MovePlayer(Direction.Down);
                        break;
                    case (int) Direction.Left:
                        _userInterface.ClearScreen();
                        _userInterface.RenderMessage("You moved left.");
                        gameBoard.MovePlayer(Direction.Left);
                        break;
                    case (int) Direction.Right:
                        _userInterface.ClearScreen();
                        _userInterface.RenderMessage("You moved right.");
                        gameBoard.MovePlayer(Direction.Right);
                        break;
                    case (int) Direction.Invalid:
                        _userInterface.RenderMessage(
                            $"{requestedMove} is not a valid move ('U','D','L', or 'R'). Please try again.");
                        continue;
                    default:
                        _userInterface.RenderMessage(
                            $"{requestedMove} is not a valid move ('U','D','L', or 'R'). Please try again.");
                        continue;
                }
                //Todo - Check for Mine (IsMined method)
                //Todo - If Mined, Explode Mine and subtract a life.
                Logger.Info("Player took a turn");
                gameBoard.DrawBoard();
                //Todo - Check Lives remaining and if none, end game, showing message to player.
                //Todo - Check if player has won. If so, end game, showing message to the player.
                //Todo - Ask to Play Again.
                //Todo - Validate Y/N
                //Todo - If Y for New Game, Start New Game.
                _userInterface.NewLine();
            }
        }
    }
}