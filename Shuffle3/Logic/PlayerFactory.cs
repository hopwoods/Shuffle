using Shuffle.Model;

namespace Shuffle.Logic
{
    class PlayerFactory : IPlayer
    {
        public Player Get()
        {
            Player player = new Player();
            return player;
        }
        public int Lives()
        {
            return 2;
        }

        public string PlayerName()
        {
            return "Player 1";
        }
    }
}
