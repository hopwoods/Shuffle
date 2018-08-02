namespace Shuffle.Model
{
    public interface IUserInterface
    {
        string GetUserInput();
        string AskForMove();
        string AskForPlayerName();
        int ValidateMove(string requestedMove);
        void ClearScreen();
        void RenderMessage(string message);
        void NewLine();
    }
}

