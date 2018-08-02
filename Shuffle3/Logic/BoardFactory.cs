using Shuffle.Model;
using System;
using System.Collections.Generic;

namespace Shuffle.Logic
{
    public class BoardFactory : IBoard
    {
        public BoardFactory()
        {
        }

        public Board Get()
        {
            Board board = new Board();
            return board;
        }
    }
}