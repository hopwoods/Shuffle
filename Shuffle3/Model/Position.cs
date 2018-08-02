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
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int row, int col)
        {
            X = row;
            Y = col;
        }
    }
}