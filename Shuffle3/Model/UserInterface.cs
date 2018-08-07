using System.Diagnostics.CodeAnalysis;
using static System.Console;
using static System.String;
using Shuffle.Utilities;

namespace Shuffle.Model
{
    public class UserInterface : IUserInterface
    {
        //Todo - Add Method Documentation
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly Utility _utility;

        public UserInterface(Utility utility)
        {
            _utility = utility;
        }

        [ExcludeFromCodeCoverage] //Cannot test for User Input
        public string GetUserInput()
        {
            string input = ReadLine();
            Logger.Info("User Typed {0}", input);
            return input;
        }

        [ExcludeFromCodeCoverage] //Cannot test writing to console.
        public void RenderMessage(string message)
        {
            WriteLine(message);
        }

        [ExcludeFromCodeCoverage] //Cannot test writing to console.
        public void NewLine()
        {
            WriteLine();
        }

        [ExcludeFromCodeCoverage] //Cannot test writing to console.
        public void ClearScreen()
        {
            Clear();
        }

        [ExcludeFromCodeCoverage] //Cannot test for user input.
        public string AskForMove()
        {
            RenderMessage("Make your move by typing 'U','D','L', or 'R' and pressing Enter");
            string requestedMove = GetUserInput();
            return requestedMove;
        }
        [ExcludeFromCodeCoverage] //Cannot test for user input.
        public string AskForPlayerName()
        {
            string requestedNameInput;
            while (true)
            {
                RenderMessage("Please Enter Your Player Name");
                requestedNameInput = GetUserInput();
                if (IsNullOrEmpty(requestedNameInput))
                {
                    ClearScreen();
                    continue;
                }
                if(_utility.IsStringTooLong(30,requestedNameInput))
                {
                    ClearScreen();
                    RenderMessage("Player Name is too long. Use 30 characters or less.");
                    continue;
                }
                break;
            }
           return requestedNameInput;
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