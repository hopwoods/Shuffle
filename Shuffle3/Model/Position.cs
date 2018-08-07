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
        //Todo - Add Position(Position) overload.

        /// <summary>
        /// Method for checking if a position is outside of the game board.
        /// </summary>
        /// <returns>True or False</returns>
        public bool IsInBounds()
        {
            //if (this == null) throw new ArgumentNullException(nameof(position));

            // ReSharper disable once JoinDeclarationAndInitializer
            bool inBounds;
            inBounds = true;

            if (X < 0)
            {
                inBounds = false;
            }
            if (X > 7)
            {
                inBounds = false;
            }
            if (Y < 0)
            {
                inBounds = false;
            }
            else if (Y > 7)
            {
                inBounds = false;
            }
            return inBounds;
        }
    }
}