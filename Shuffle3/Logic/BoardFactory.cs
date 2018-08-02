using Shuffle3.Model;

namespace Shuffle3.Logic
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