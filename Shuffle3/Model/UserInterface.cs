using System.Diagnostics.CodeAnalysis;
using static System.Console;
using static System.String;
using Shuffle.Utilities;

namespace Shuffle.Model
{
    public class UserInterface : IUserInterface
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly Utility _utility;

        public UserInterface(Utility utility)
        {
            _utility = utility;
        }

        /// <summary>
        /// Get and return user keyboard input
        /// </summary>
        /// <returns></returns>
        [ExcludeFromCodeCoverage] //Cannot test for User Input
        public string GetUserInput()
        {
            string input = ReadLine();
            Logger.Info("User Typed {0}", input);
            return input;
        }

        /// <summary>
        /// Write a line to the console.
        /// </summary>
        /// <param name="message"></param>
        [ExcludeFromCodeCoverage] //Cannot test writing to console.
        public void RenderMessage(string message)
        {
            WriteLine(message);
        }

        /// <summary>
        /// Write a emtpy line to the console.
        /// </summary>
        [ExcludeFromCodeCoverage] //Cannot test writing to console.
        public void NewLine()
        {
            WriteLine();
        }

        /// <summary>
        /// Clear the console.
        /// </summary>
        [ExcludeFromCodeCoverage] //Cannot test writing to console.
        public void ClearScreen()
        {
            Clear();
        }

        /// <summary>
        /// Ask the player to key in their move.
        /// </summary>
        /// <returns>String containing the user's input.</returns>
        [ExcludeFromCodeCoverage] //Cannot test for user input.
        public string AskForMove()
        {
            RenderMessage("Make your move by typing 'U','D','L', or 'R' and pressing Enter");
            string requestedMove = GetUserInput();
            return requestedMove;
        }

        /// <summary>
        /// Ask for the player's name return as a string.
        /// </summary>
        /// <returns>String containing the players input.</returns>
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
                if (_utility.IsStringTooLong(30, requestedNameInput))
                {
                    ClearScreen();
                    RenderMessage("Player Name is too long. Use 30 characters or less.");
                    continue;
                }
                break;
            }
            return requestedNameInput;
        }

        /// <summary>
        /// Validate the keyboard input against an allowed list of values.
        ///  </summary>
        /// <param name="requestedMove"></param>
        /// <returns>Returns a direction or invalid direction.</returns>
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

        [ExcludeFromCodeCoverage] //Cannot test for user input
        public string AskToPlayAgain()
        {
            string playerResponse;
            while (true)
            {
                RenderMessage("Would you like to play again? Type 'Y' or 'N' and press enter.");
                playerResponse = GetUserInput().ToUpper();
                if (IsNullOrEmpty(playerResponse))
                {
                    ClearScreen();
                    continue;
                }
                if (playerResponse != "Y" && playerResponse != "N")
                {
                    ClearScreen();
                    RenderMessage("That's wrong! Please only type 'Y' or 'N' and press enter.");
                    NewLine();
                    continue;
                }
                break;
            }
            return playerResponse;
        }

        public bool ValidatePlayAgainResponse(string response)
        {
            switch (response)
            {
                default:
                    return false;
                case "Y":
                    return true;
                case "N":
                    return false;
            }
        }
    }
}