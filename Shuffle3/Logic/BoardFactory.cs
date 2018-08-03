using Shuffle.Model;

namespace Shuffle.Logic
{
    public class BoardFactory : IBoardFactory
    {
        public Board Get()
        {
            Board board = new Board();
            return board;
        }
    }
}