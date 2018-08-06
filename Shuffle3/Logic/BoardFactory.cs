using Shuffle.Model;

namespace Shuffle.Logic
{
    public class BoardFactory : IBoardFactory
    {
        /// <summary>
        /// Generate a new game board.
        /// </summary>
        /// <returns>A Game Board Object</returns>
        public Board CreateBoard()
        {
            Board board = new Board();
            return board;
        }
    }
}