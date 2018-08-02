namespace Shuffle.Model
{
    public interface IUserInterface
    {
        string GetUserInput();
        void RenderMessage(string message);
        void NewLine();
        string AskForMove();
        int ValidateMove(string requestedMove);
        void ClearScreen();
    }
}

