using System;
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
        Player
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
            {CellStatus.Mine, new CellFormat {DisplayCharacter = Convert.ToChar("\u25CF"), DisplayColour = Red}},
            {CellStatus.Player, new CellFormat {DisplayCharacter = Convert.ToChar("\u2302"), DisplayColour = Yellow}},
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
            //Console.WriteLine($"Player Position is {PlayerPosition.X},{PlayerPosition.Y}");
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
        private static char GetLetterFromX(int x) => (char) x;

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
        public void SetCell(Position position, CellStatus value) //Todo - Add SetCell Overload tests.
        {
            Cells[position.X, position.Y] = (int) value;
            Logger.Info($"Cell {position.X},{position.Y} set to status {value}");
        }

        /// <summary>
        /// Set the player start position
        /// </summary>
        private void PlacePlayerStartPosition()
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
        private int GenerateMines()
        {
            int mines = _random.Next(2, 6); //Minimum number of mines is two.
            Logger.Info($"{mines} Mines Generated");
            return mines;
        }

        /// <summary>
        /// Place mines in random cell locations on the board.
        /// </summary>
        /// <param name="mines"></param>
        private void PlaceMines(int mines)
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
        /// Set the current known player position on the board.
        /// </summary>
        /// <param name="newPosition"></param>
        public void SetCurrentPlayerPosition(Position newPosition)
        {
            //Position playerPosition = new Position(x, y);
            PlayerPosition = newPosition;
            Logger.Info($"Player Position Set to {PlayerPosition.X},{PlayerPosition.Y}.");
        }

        /// <summary>
        /// Move the player on the board by one cell, in a given direction.
        /// </summary>
        /// <param name="direction"></param>
        public string MovePlayer(Direction direction)
        {
            //Todo - DRY PRINCIPLE. Try to make more modular
            Direction moveDirection = direction;
            bool isInBounds;
            string moveMessage = null;
            Position newPosition;
            switch (moveDirection)
            {
                default:
                    Logger.Warn("Move Direction is not supported");
                    break;
                case Direction.Up:
                    newPosition = new Position(PlayerPosition.X - 1, PlayerPosition.Y);
                    isInBounds = newPosition.IsInBounds();
                    if (isInBounds)
                    {
                        SetCell(PlayerPosition.X, PlayerPosition.Y,
                            CellStatus.Empty); //Todo - Create ClearCurrentPosition method
                        SetCell(newPosition.X, newPosition.Y,
                            CellStatus.Player); //Todo - Create MoveToNewPosition method
                        SetCurrentPlayerPosition(newPosition);
                        moveMessage = $"You moved {moveDirection}.";
                    }
                    else
                    {
                        moveMessage = ($"You can't move {moveDirection}. Try Again");
                    }
                    break;
                case Direction.Down:
                    newPosition = new Position(PlayerPosition.X + 1, PlayerPosition.Y);
                    isInBounds = newPosition.IsInBounds();
                    if (isInBounds)
                    {
                        SetCell(PlayerPosition.X, PlayerPosition.Y, CellStatus.Empty);
                        SetCell(newPosition.X, newPosition.Y, CellStatus.Player);
                        SetCurrentPlayerPosition(newPosition);
                        moveMessage = $"You moved {moveDirection}.";
                    }
                    else
                    {
                        moveMessage = ($"You can't move {moveDirection}. Try Again");
                    }
                    break;
                case Direction.Left:
                    newPosition = new Position(PlayerPosition.X, PlayerPosition.Y - 1);
                    isInBounds = newPosition.IsInBounds();
                    if (isInBounds)
                    {
                        SetCell(PlayerPosition.X, PlayerPosition.Y, CellStatus.Empty);
                        SetCell(newPosition.X, newPosition.Y, CellStatus.Player);
                        SetCurrentPlayerPosition(newPosition);
                        moveMessage = $"You moved {moveDirection}.";
                    }
                    else
                    {
                        moveMessage = ($"You can't move {moveDirection}. Try Again");
                    }
                    break;
                case Direction.Right:
                    newPosition = new Position(PlayerPosition.X, PlayerPosition.Y + 1);
                    isInBounds = newPosition.IsInBounds();
                    if (isInBounds)
                    {
                        SetCell(PlayerPosition.X, PlayerPosition.Y, CellStatus.Empty);
                        SetCell(newPosition.X, newPosition.Y, CellStatus.Player);
                        SetCurrentPlayerPosition(newPosition);
                        moveMessage = $"You moved {moveDirection}.";
                    }
                    else
                    {
                        moveMessage = ($"You can't move {moveDirection}. Try Again");
                    }
                    break;
                case Direction.Invalid:
                    Logger.Warn("Move Direction is Invalid");
                    break;
            }
            WriteLine(moveMessage);
            WriteLine();
            Logger.Info($"Player moved one cell {moveDirection} to {PlayerPosition.X},{PlayerPosition.Y}");
            return moveMessage;
        }

        //Todo - Add a IsMined method to check for a hidden mine.
        //Todo - Add a method to 'Explode' a mine. Change Status from Hidden to Mine.
        //Todo - Add a method to check if player position is in top row.
    }

    #endregion
}