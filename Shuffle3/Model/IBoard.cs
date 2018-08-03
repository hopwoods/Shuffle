namespace Shuffle.Model
{
    public interface IBoard
    {
        bool DrawBoard();
        void SetCell(int x, int y, CellStatus value);
        void SetCurrentPlayerPosition(Position newPosition);
        string MovePlayer(Direction direction);
    }
}
