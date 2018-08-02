using System.Diagnostics.CodeAnalysis;

namespace Shuffle.Model
{
    [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
    internal interface IPlayer
    {
        void SetPlayerName(string playername);
        void SetLives(int lives);
    }
}