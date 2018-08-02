using Shuffle.Model;

namespace Shuffle.Logic
{
    public class BoardFactory : IBoard
    {
        public Board Get()
        {
            Board board = new Board();
            return board;
        }
    }
}