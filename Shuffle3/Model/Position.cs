namespace Shuffle3.Model
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

        public Position(int row, int col)
        {
            X = row;
            Y = col;
        }
    }
}