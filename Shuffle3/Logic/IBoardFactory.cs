using Shuffle.Model;

namespace Shuffle.Logic
{
    public interface IBoardFactory
    {
        /// <summary>
        /// Generate a new game board.
        /// </summary>
        /// <returns>A Game Board Object</returns>
        Board CreateBoard();
    }
}
