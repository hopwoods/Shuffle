using System;
using System.Collections.Generic;

namespace Shuffle3.Model
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

    public class CellFormat
    {
        public string DisplayCharacter { get; set; }
        public ConsoleColor DisplayColour { get; set; }
    }

    public class Board
    {
        #region Fields

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private Position _playerPosition;
        public readonly int[,] Cells;
        private readonly Random _random;

        private readonly Dictionary<CellStatus, CellFormat> _cellFormats = new Dictionary<CellStatus, CellFormat>()
        {
            {CellStatus.Empty, new CellFormat {DisplayCharacter = " ", DisplayColour = ConsoleColor.Black}},
            {CellStatus.HiddenMine, new CellFormat {DisplayCharacter = " ", DisplayColour = ConsoleColor.Black}},
            {CellStatus.Mine, new CellFormat {DisplayCharacter = "M", DisplayColour = ConsoleColor.Red}},
            {CellStatus.Player, new CellFormat {DisplayCharacter = "O", DisplayColour = ConsoleColor.Yellow}},
        };

        private readonly ConsoleColor _foregroundColor = ConsoleColor.DarkGray;

        #endregion

        #region Methods

        /// <summary>
        /// Board object for the game.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ApplicationException"></exception>
        public Board()
        {
            int width = 8;
            int height = 8;
            Cells = new int[width, height];
            _random = new Random();
            int mines = GenerateMines();
            PlaceMines(mines);
            PlacePlayerStartPosition();
        }

        /// <summary>
        ///     Method for drawing the game board to the screen, along with values for each cell.
        /// </summary>
        public bool DrawBoard()
        {
            //Draw X Axis Coordinate values in first row, e.g. [A][B][C][D][E][F] etc
            Console.ForegroundColor = _foregroundColor;
            Console.Write("   ");
            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                char x = GetLetterFromX(i + 65);
                Console.Write($"[{x}]");
            }

            //For each row, draw Y axis values [1][2][3][4][5] etc, and then each cell in the row.
            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                Console.WriteLine();
                Console.Write($"[{i + 1}]");
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    int cellValue = Cells[i, j];
                    FormatCell(cellValue);
                }
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            //Console.WriteLine();
            //Console.WriteLine($"Player Position is {PlayerPosition.X},{PlayerPosition.Y}");
            Logger.Info("Board Drawn");
            return true;
        }

        public int FormatCell(int cellValue)
        {
            switch (cellValue)
            {
                case (int) CellStatus.Mine:
                    Console.ForegroundColor = _foregroundColor;
                    Console.Write("[");
                    Console.ForegroundColor = _cellFormats[CellStatus.Mine].DisplayColour;
                    Console.Write(_cellFormats[CellStatus.Mine].DisplayCharacter);
                    Console.ForegroundColor = _foregroundColor;
                    Console.Write("]");
                    return cellValue;
                case (int) CellStatus.HiddenMine:
                    Console.ForegroundColor = _foregroundColor;
                    Console.Write("[");
                    Console.ForegroundColor = _cellFormats[CellStatus.HiddenMine].DisplayColour;
                    Console.Write(_cellFormats[CellStatus.HiddenMine].DisplayCharacter);
                    Console.ForegroundColor = _foregroundColor;
                    Console.Write("]");
                    return cellValue;
                case (int) CellStatus.Player:
                    Console.ForegroundColor = _foregroundColor;
                    Console.Write("[");
                    Console.ForegroundColor = _cellFormats[CellStatus.Player].DisplayColour;
                    Console.Write(_cellFormats[CellStatus.Player].DisplayCharacter);
                    Console.ForegroundColor = _foregroundColor;
                    Console.Write("]");
                    return cellValue;
                default:
                    Console.ForegroundColor = _foregroundColor;
                    Console.Write("[");
                    Console.ForegroundColor = _cellFormats[CellStatus.Empty].DisplayColour;
                    Console.Write(_cellFormats[CellStatus.Empty].DisplayCharacter);
                    Console.ForegroundColor = _foregroundColor;
                    Console.Write("]");
                    return cellValue;
            }
        }

        private static char GetLetterFromX(int x)
        {
            return (char) x;
        }

        private void SetCell(int x, int y, CellStatus value)
        {
            Cells[x, y] = (int) value;
        }

        private void PlacePlayerStartPosition()
        {
            SetCell(7, 0, CellStatus.Player);
            SetCurrentPlayerPosition(7, 0);
        }

        private int GenerateMines()
        {
            int mines = _random.Next(1, 5);
            return mines;
        }

        private void PlaceMines(int mines)
        {
            for (int i = 0; i < mines; i++)
            {
                int x = _random.Next(0, 7);
                int y = _random.Next(0, 7);
                SetCell(x, y, CellStatus.HiddenMine);
            }
        }

        private void SetCurrentPlayerPosition(int x, int y)
        {
            Position playerPosition = new Position(x, y);
            _playerPosition = playerPosition;
        }

        public void MovePlayer(Direction direction)
        {
            Direction moveDirection = direction;
            switch (moveDirection) //Todo - Add Out of Range Check to stop player moving of edges
            {
                case Direction.Up:
                    SetCell(_playerPosition.X, _playerPosition.Y, CellStatus.Empty);
                    SetCell(_playerPosition.X - 1, _playerPosition.Y, CellStatus.Player);
                    SetCurrentPlayerPosition(_playerPosition.X - 1, _playerPosition.Y);
                    break;
                case Direction.Down:
                    SetCell(_playerPosition.X, _playerPosition.Y, CellStatus.Empty);
                    SetCell(_playerPosition.X + 1, _playerPosition.Y, CellStatus.Player);
                    SetCurrentPlayerPosition(_playerPosition.X + 1, _playerPosition.Y);
                    break;
                case Direction.Left:
                    SetCell(_playerPosition.X, _playerPosition.Y, CellStatus.Empty);
                    SetCell(_playerPosition.X, _playerPosition.Y - 1, CellStatus.Player);
                    SetCurrentPlayerPosition(_playerPosition.X, _playerPosition.Y - 1);
                    break;
                case Direction.Right:
                    SetCell(_playerPosition.X, _playerPosition.Y, CellStatus.Empty);
                    SetCell(_playerPosition.X, _playerPosition.Y + 1, CellStatus.Player);
                    SetCurrentPlayerPosition(_playerPosition.X, _playerPosition.Y + 1);
                    break;
            }
        }
    }

    #endregion
}