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

        [ExcludeFromCodeCoverage] //Cannot Test for User Input
        public void StartGame()
        {
            try
            {
                Board gameBoard = _boardFactory.Get();
                Logger.Info("New Game Board created");
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
                        gameBoard.DrawBoard();
                        _userInterface.NewLine();
                        break;
                    case (int) Direction.Down:
                        _userInterface.ClearScreen();
                        _userInterface.RenderMessage("You moved down.");
                        gameBoard.MovePlayer(Direction.Down);
                        gameBoard.DrawBoard();
                        _userInterface.NewLine();
                        break;
                    case (int) Direction.Left:
                        _userInterface.ClearScreen();
                        _userInterface.RenderMessage("You moved left.");
                        gameBoard.MovePlayer(Direction.Left);
                        gameBoard.DrawBoard();
                        _userInterface.NewLine();
                        break;
                    case (int) Direction.Right:
                        _userInterface.ClearScreen();
                        _userInterface.RenderMessage("You moved right.");
                        gameBoard.MovePlayer(Direction.Right);
                        gameBoard.DrawBoard();
                        _userInterface.NewLine();
                        break;
                    case (int) Direction.Invalid:
                        _userInterface.RenderMessage(
                            $"{requestedMove} is not a valid move ('U','D','L', or 'R'). Please try again.");
                        break;
                    default:
                        _userInterface.RenderMessage(
                            $"{requestedMove} is not a valid move ('U','D','L', or 'R'). Please try again.");
                        break;
                }
            }
        }
    }
}