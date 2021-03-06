﻿using System;
using System.Collections.Generic;
using static System.Console;
using static System.ConsoleColor;

namespace Shuffle.Model
{
    /// <summary>
    /// Constants for contents of a cell
    /// </summary>
    public enum CellStatus
    {
        Empty,
        HiddenMine,
        Mine,
        Player,
        PlayerIsHit
    }

    /// <summary>
    /// Game Board Object and Method
    /// </summary>
    public class Board : IBoard
    {
        #region Fields

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public Position PlayerPosition;
        public readonly int[,] Cells;
        private readonly Random _random;
        private const ConsoleColor ForegroundColor = DarkGray;

        private readonly Dictionary<CellStatus, CellFormat> _cellFormats = new Dictionary<CellStatus, CellFormat>()
        {
            {CellStatus.Empty, new CellFormat {DisplayCharacter = Convert.ToChar(" "), DisplayColour = Black}},
            {CellStatus.HiddenMine, new CellFormat {DisplayCharacter = Convert.ToChar(" "), DisplayColour = Black}},
            {CellStatus.Mine, new CellFormat {DisplayCharacter = Convert.ToChar("\u2300"), DisplayColour = Red}},
            {CellStatus.Player, new CellFormat {DisplayCharacter = Convert.ToChar("\u2302"), DisplayColour = Yellow}},
            {CellStatus.PlayerIsHit, new CellFormat {DisplayCharacter = Convert.ToChar("\u2302"), DisplayColour = Red}},
        };

        #endregion

        #region Methods

        /// <summary>
        /// Board object for the game.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ApplicationException"></exception>
        public Board()
        {
            const int width = 8;
            const int height = 8;
            Cells = new int[width, height];
            _random = new Random();
            int mines = GenerateMines();
            PlacePlayerStartPosition();
            PlaceMines(mines);
        }

        /// <summary>
        ///     Method for drawing the game board to the screen, along with values for each cell.
        /// </summary>
        public bool DrawBoard()
        {
            //Draw X Axis Coordinate values in first row, e.g. [A][B][C][D][E][F] etc
            Console.ForegroundColor = DarkCyan;
            Write("   ");
            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                char x = GetLetterFromX(i + 65);
                Write($"[{x}]");
            }

            //For each row, draw Y axis values [1][2][3][4][5] etc, and then each cell in the row.
            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                WriteLine();
                Console.ForegroundColor = DarkCyan;
                Write($"[{i + 1}]");
                Console.ForegroundColor = ForegroundColor;
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    int cellValue = Cells[i, j];
                    FormatCell(cellValue);
                }
            }

            Console.ForegroundColor = Gray;
            WriteLine();
            //WriteLine($"Player Position is {PlayerPosition.X},{PlayerPosition.Y}");
            Logger.Info("Board Drawn");
            return true;
        }

        /// <summary>
        /// Format a cell based on the cell value (status)
        /// </summary>
        /// <param name="cellValue"></param>
        /// <returns></returns>
        public int FormatCell(int cellValue)
        {
            switch (cellValue)
            {
                case (int) CellStatus.Mine:
                    Console.ForegroundColor = ForegroundColor;
                    Write("[");
                    Console.ForegroundColor = _cellFormats[CellStatus.Mine].DisplayColour;
                    Write(_cellFormats[CellStatus.Mine].DisplayCharacter);
                    Console.ForegroundColor = ForegroundColor;
                    Write("]");
                    return cellValue;
                case (int) CellStatus.HiddenMine:
                    Console.ForegroundColor = ForegroundColor;
                    Write("[");
                    Console.ForegroundColor = _cellFormats[CellStatus.HiddenMine].DisplayColour;
                    Write(_cellFormats[CellStatus.HiddenMine].DisplayCharacter);
                    Console.ForegroundColor = ForegroundColor;
                    Write("]");
                    return cellValue;
                case (int) CellStatus.Player:
                    Console.ForegroundColor = ForegroundColor;
                    Write("[");
                    Console.ForegroundColor = _cellFormats[CellStatus.Player].DisplayColour;
                    Write(_cellFormats[CellStatus.Player].DisplayCharacter);
                    Console.ForegroundColor = ForegroundColor;
                    Write("]");
                    return cellValue;
                case (int) CellStatus.PlayerIsHit:
                    Console.ForegroundColor = ForegroundColor;
                    Write("[");
                    Console.ForegroundColor = _cellFormats[CellStatus.PlayerIsHit].DisplayColour;
                    Write(_cellFormats[CellStatus.PlayerIsHit].DisplayCharacter);
                    Console.ForegroundColor = ForegroundColor;
                    Write("]");
                    return cellValue;
                default:
                    Console.ForegroundColor = ForegroundColor;
                    Write("[");
                    Console.ForegroundColor = _cellFormats[CellStatus.Empty].DisplayColour;
                    Write(_cellFormats[CellStatus.Empty].DisplayCharacter);
                    Console.ForegroundColor = ForegroundColor;
                    Write("]");
                    return cellValue;
            }
        }

        /// <summary>
        /// Get letter of Alphabet from integer.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public char GetLetterFromX(int x) => (char) x;

        /// <summary>
        /// Set a cell value by providing cell XY coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="value"></param>
        public void SetCell(int x, int y, CellStatus value)
        {
            Cells[x, y] = (int) value;
            Logger.Info($"Cell {x},{y} set to status {value}");
        }

        /// <summary>
        /// Set a cell value by providing position object
        /// </summary>
        /// <param name="value"></param>
        /// <param name="position"></param>
        public void SetCell(Position position, CellStatus value)
        {
            Cells[position.X, position.Y] = (int) value;
            Logger.Info($"Cell {position.X},{position.Y} set to status {value}");
        }

        /// <summary>
        /// Set the player start position
        /// </summary>
        public void PlacePlayerStartPosition()
        {
            Position startPosition = new Position(7, 0);
            SetCell(startPosition.X, startPosition.Y, CellStatus.Player);
            SetCurrentPlayerPosition(startPosition);
            Logger.Info("Player Start Position Set to 7,0");
        }

        /// <summary>
        /// Generate a random number of mines to place on the board. Min of 2, Max of 6.
        /// Min of 2 so game is always able to be lost.
        /// </summary>
        /// <returns>Integer for Number of Mines</returns>
        public int GenerateMines()
        {
            int mines = _random.Next(4, 8); //Minimum number of mines is two.
            Logger.Info($"{mines} Mines Generated");
            return mines;
        }

        /// <summary>
        /// Place mines in random cell locations on the board.
        /// </summary>
        /// <param name="mines"></param>
        public void PlaceMines(int mines)
        {
            for (int i = 0; i < mines; i++)
            {
                int x = _random.Next(0, 7);
                int y = _random.Next(0, 7);
                if (Cells[x, y] == (int) CellStatus.Empty) //Only Place Mine on empty cells.
                {
                    SetCell(x, y, CellStatus.HiddenMine);
                }
            }

            Logger.Info($"{mines} Mines Placed");
        }

        /// <summary>
        /// Clear Cell. Set to Empty or to Mine if player landed on a mine.
        /// </summary>
        /// <param name="position"></param>
        public void ClearCell(Position position)
        {
            switch (Cells[position.X, position.Y])
            {
                case (int) CellStatus.PlayerIsHit:
                    SetCell(position, CellStatus.Mine);
                    break;
                default:
                    SetCell(position, CellStatus.Empty);
                    break;
            }
        }

        /// <summary>
        /// Move the player piece (Change Status to Player) to the given cell.
        /// </summary>
        /// <param name="position"></param>
        public void MoveToCell(Position position) //Todo - Add tests for Mines and PlayerHit
        {
            int cellStatus = GetCellStatus(position);
            if(cellStatus != (int) CellStatus.PlayerIsHit)
            {
                Cells[position.X, position.Y] = (int) CellStatus.Player;
                Logger.Info($"Player moved to {position.X},{position.Y}.");
            }
            if(cellStatus == (int) CellStatus.Mine)
            {
                Cells[position.X, position.Y] = (int) CellStatus.PlayerIsHit;
                Logger.Info($"Player moved to {position.X},{position.Y}.");
            }
           
        }

        /// <summary>
        /// Set the current known player position on the board.
        /// </summary>
        /// <param name="newPosition"></param>
        public void SetCurrentPlayerPosition(Position newPosition)
        {
            PlayerPosition = newPosition;
            Logger.Info($"Player Position Set to {PlayerPosition.X},{PlayerPosition.Y}.");
        }

        /// <summary>
        /// Check if a cell contains a hidden mine.
        /// </summary>
        /// <param name="position"></param>
        /// <returns>True</returns>
        public bool IsCellMined(Position position)
        {
            int cellStatus = GetCellStatus(position);
            if (cellStatus == (int) CellStatus.HiddenMine) return true;
            Logger.Info($"Cell {position.X}, {position.Y} a Mine.");
            return false;
        }

        /// <summary>
        /// Retrieve the status of a given cell.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public int GetCellStatus(Position position)
        {
            int cellStatus = Cells[position.X, position.Y];
            Logger.Info($"Cell {position.X}, {position.Y} has a Status of: {cellStatus}");
            return cellStatus;
        }

        public bool IsCellInTopRow(Position position)
        {
            return position.X == 0;
        }

        /// <summary>
        /// Move the player on the board by one cell, in a given direction.
        /// </summary>
        /// <param name="direction"></param>
        public string MovePlayer(Direction direction)
        {
            
            bool isInBounds;
            bool isCellMined;
            string moveMessage = null;
            Position newPosition;
            switch (direction)
            {
                default:
                    Logger.Warn("Move Direction is not supported");
                    break;
                case Direction.Up:
                    newPosition = new Position(PlayerPosition.X - 1, PlayerPosition.Y);
                    isInBounds = newPosition.IsInBounds();
                    if (isInBounds)
                    {
                        isCellMined = IsCellMined(newPosition);
                        if(isCellMined)
                        {
                            Explode(newPosition);
                        }
                        ClearCell(PlayerPosition);
                        MoveToCell(newPosition);
                        SetCurrentPlayerPosition(newPosition);
                        moveMessage = $"You moved {direction}.";
                        
                    }
                    else
                    {
                        moveMessage = ($"You can't move {direction}. Try Again");
                    }

                    break;
                case Direction.Down:
                    newPosition = new Position(PlayerPosition.X + 1, PlayerPosition.Y);
                    isInBounds = newPosition.IsInBounds();
                    if (isInBounds)
                    {
                        isCellMined = IsCellMined(newPosition);
                        if(isCellMined)
                        {
                            Explode(newPosition);
                        }
                        ClearCell(PlayerPosition);
                        MoveToCell(newPosition);
                        SetCurrentPlayerPosition(newPosition);
                        moveMessage = $"You moved {direction}.";
                        
                    }
                    else
                    {
                        moveMessage = ($"You can't move {direction}. Try Again");
                    }

                    break;
                case Direction.Left:
                    newPosition = new Position(PlayerPosition.X, PlayerPosition.Y - 1);
                    isInBounds = newPosition.IsInBounds();
                    if (isInBounds)
                    {
                        isCellMined = IsCellMined(newPosition);
                        if(isCellMined)
                        {
                            Explode(newPosition);
                        }
                        ClearCell(PlayerPosition);
                        MoveToCell(newPosition);
                        SetCurrentPlayerPosition(newPosition);
                        moveMessage = $"You moved {direction}.";
                        
                    }
                    else
                    {
                        moveMessage = ($"You can't move {direction}. Try Again");
                    }

                    break;
                case Direction.Right:
                    newPosition = new Position(PlayerPosition.X, PlayerPosition.Y + 1);
                    isInBounds = newPosition.IsInBounds();
                    if (isInBounds)
                    {
                        isCellMined = IsCellMined(newPosition);
                        if(isCellMined)
                        {
                            Explode(newPosition);
                        }
                        ClearCell(PlayerPosition);
                        MoveToCell(newPosition);
                        SetCurrentPlayerPosition(newPosition);
                        moveMessage = $"You moved {direction}.";
                        
                    }
                    else
                    {
                        moveMessage = ($"You can't move {direction}. Try Again");
                    }

                    break;
                case Direction.Invalid:
                    Logger.Warn("Move Direction is Invalid");
                    break;
            }

            WriteLine(moveMessage);
            WriteLine();
            Logger.Info($"Player moved one cell {direction} to {PlayerPosition.X},{PlayerPosition.Y}");
            return moveMessage;
        }

        /// <summary>
        /// Explode a hidden mine by changing the status from HiddenMine to PlayerIsHit
        /// </summary>
        /// <param name="position"></param>
        public void Explode(Position position)
        {
            int cellStatus = GetCellStatus(position);
            if (cellStatus == (int) CellStatus.HiddenMine)
            {
                SetCell(position, CellStatus.PlayerIsHit);
                WriteLine("Oh Noes! You hit a mine, you lose a life.");
                Logger.Info($"Player Landed on a Mine. Cell {position.X}, {position.Y}");
            }
        }
    }

    #endregion
}