﻿using System;
using System.Diagnostics.CodeAnalysis;
using NLog;
using Shuffle.Model;

namespace Shuffle.Logic
{
    public class GameProcessor
    {
        #region Fields

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IUserInterface _userInterface;
        private readonly IBoardFactory _boardFactory;
        private readonly IPlayerFactory _playerFactory;

        #endregion

        #region Constructor

        public GameProcessor(IUserInterface userInterface, IBoardFactory boardFactory, IPlayerFactory playerFactory)
        {
            _userInterface = userInterface;
            _boardFactory = boardFactory;
            _playerFactory = playerFactory;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Start the Game.
        /// </summary>
        [ExcludeFromCodeCoverage] //Cannot Test for User Input
        public void StartGame()
        {
            try
            {
                Board gameBoard = _boardFactory.CreateBoard();
                Logger.Info("New Game Board created");
                Player player = _playerFactory.CreatePlayer();
                _userInterface.RenderMessage("Shuffle! New Game Started.");
                player.SetPlayerName(_userInterface.AskForPlayerName());
                player.SetLives();
                Logger.Info("New Player Created");
                _userInterface.ClearScreen();
                _userInterface.RenderMessage($"Welcome {player.Name} to Shuffle!");
                _userInterface.RenderMessage(
                    "Move your piece to the top of the board to win. Watch out for mines, hit two and its GAME OVER!");
                _userInterface.NewLine();
                bool success = gameBoard.DrawBoard();
                if (!success)
                {
                    throw new ApplicationException("Something went wrong while drawing the board");
                }

                _userInterface.NewLine();
                _userInterface.RenderMessage("Ready Player One.");
                Logger.Info("Turns Started");
                TakeTurns(gameBoard, player);

                string askToPlayAgainResult = _userInterface.AskToPlayAgain();
                bool isNewGameWanted = _userInterface.ValidatePlayAgainResponse(askToPlayAgainResult);
                if(isNewGameWanted)
                {
                    _userInterface.ClearScreen();
                    StartGame();
                }
                else
                {
                    _userInterface.ClearScreen();
                    _userInterface.RenderMessage($"Thanks for playing {player.Name}. Press any key to exit and press enter");
                }
                _userInterface.GetUserInput();
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
        /// <param name="player"></param>
        [ExcludeFromCodeCoverage] //Cannot Test for User Input
        private void TakeTurns(Board gameBoard, Player player)
        {
            while (true)
            {
                string requestedMove = _userInterface.AskForMove();
                int move = _userInterface.ValidateMove(requestedMove);
                switch (move)
                {
                    case (int) Direction.Up:
                        _userInterface.ClearScreen();
                        gameBoard.MovePlayer(Direction.Up);
                        break;
                    case (int) Direction.Down:
                        _userInterface.ClearScreen();
                        gameBoard.MovePlayer(Direction.Down);
                        break;
                    case (int) Direction.Left:
                        _userInterface.ClearScreen();
                        gameBoard.MovePlayer(Direction.Left);
                        break;
                    case (int) Direction.Right:
                        _userInterface.ClearScreen();
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

                int cellStatus = gameBoard.GetCellStatus(gameBoard.PlayerPosition);
                if (cellStatus == (int) CellStatus.PlayerIsHit)
                {
                    player.LoseLife();
                }

                Logger.Info("Player took a turn");
                gameBoard.DrawBoard();
                if (!player.IsPlayerAlive())
                {
                    Logger.Info($"Player: {player.Name} Died. Ending Turns.");
                    _userInterface.RenderMessage($"{player.Name} you have no lives left! Game Over Man, Game Over.");
                    _userInterface.NewLine();
                    break;
                }

                bool hasPlayerWon = gameBoard.IsCellInTopRow(gameBoard.PlayerPosition);
                if (hasPlayerWon)
                {
                    Logger.Info($"Player: {player.Name} Won the game. Ending Turns.");
                    _userInterface.RenderMessage($"{player.Name} you Won! Congratualtions!");
                    _userInterface.NewLine();
                    break;
                }
            }
        }

        #endregion
    }
}