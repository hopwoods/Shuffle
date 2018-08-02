namespace Shuffle.Model
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        Invalid
    }

    public class Position
    {
        public int X { get; }
        public int Y { get; }

        /// <summary>
        /// Position Object for referencing a cell on the board.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public Position(int row, int col)
        {
            X = row;
            Y = col;
        }
    }
}