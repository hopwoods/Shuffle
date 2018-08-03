using Shuffle.Model;

namespace Shuffle.Logic
{
    public class PlayerFactory : IPlayerFactory
    {
        public Player CreatePlayer()
        {
            Player player = new Player();
            return player;
        }
    }
}
