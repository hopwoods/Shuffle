using System;
using System.Diagnostics.CodeAnalysis;

namespace Shuffle.Model
{
    public class UserInterface : IUserInterface
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        [ExcludeFromCodeCoverage] //Cannot test for User Input
        public string GetUserInput()
        {
            string input = Console.ReadLine();
            Logger.Info("User Typed {0}", input);
            return input;
        }

        [ExcludeFromCodeCoverage] //Cannot test writing to console.
        public void RenderMessage(string message)
        {
            Console.WriteLine(message);
        }

        [ExcludeFromCodeCoverage] //Cannot test writing to console.
        public void NewLine()
        {
            Console.WriteLine();
        }
        [ExcludeFromCodeCoverage] //Cannot test writing to console.
        public void ClearScreen()
        {
            Console.Clear();
        }

        [ExcludeFromCodeCoverage] //Cannot test for user input.
        public string AskForMove()
        {
            RenderMessage("Make your move by typing 'U','D','L', or 'R' and pressing Enter");
            string requestedMove = GetUserInput();
            return requestedMove;
        }

        public int ValidateMove(string requestedMove)
        {
            string move = requestedMove.ToUpper();
            switch (move)
            {
                case "U":
                    return (int) Direction.Up;
                case "D":
                    return (int) Direction.Down;
                case "L":
                    return (int) Direction.Left;
                case "R":
                    return (int) Direction.Right;
                default: //Anything Else
                    return (int) Direction.Invalid;
            }
        }
    }
}