using System.Diagnostics.CodeAnalysis;

namespace Shuffle.Model
{
    [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
    public interface IBoard
    {
        bool DrawBoard();
        void SetCell(int x, int y, CellStatus value);
        void SetCurrentPlayerPosition(Position newPosition);
        string MovePlayer(Direction direction);
    }
}
