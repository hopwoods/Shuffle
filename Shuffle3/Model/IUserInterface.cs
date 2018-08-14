namespace Shuffle.Model
{
    public interface IUserInterface
    {
        string GetUserInput();
        string AskForMove();
        string AskForPlayerName();
        string AskToPlayAgain();
        bool ValidatePlayAgainResponse(string response);
        int ValidateMove(string requestedMove);
        void ClearScreen();
        void RenderMessage(string message);
        void NewLine();
    }
}

