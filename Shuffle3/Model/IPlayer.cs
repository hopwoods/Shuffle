using System.Diagnostics.CodeAnalysis;

namespace Shuffle.Model
{
    [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
    public interface IPlayer
    {
        void SetPlayerName(string playername);
        void SetLives(int lives);
        bool IsPlayerAlive();
        void LoseLife();
    }
}