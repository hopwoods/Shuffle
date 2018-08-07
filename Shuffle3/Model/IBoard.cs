using System;
using System.Diagnostics.CodeAnalysis;

namespace Shuffle.Model
{
    [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
    public interface IBoard
    {
        bool DrawBoard();
        void SetCell(int x, int y, CellStatus value);
        void SetCell(Position position, CellStatus value);
        void SetCurrentPlayerPosition(Position newPosition);
        void PlacePlayerStartPosition();
        void PlaceMines(int mines);
        void ClearCell(Position cell);
        void MoveToCell(Position position);
        void Explode(Position position);
        int GetCellStatus(Position position);
        int GenerateMines();
        int FormatCell(int cellValue);
        bool IsCellInTopRow(Position position);
        bool IsCellMined(Position position);
        char GetLetterFromX(int x);
        string MovePlayer(Direction direction);
    }
}
