using System;

namespace Shuffle.Model
{
    public class Player : IPlayer
    {
        #region Fields

        public int Lives;
        public string Name;

        #endregion

        #region Methods

        /// <summary>
        /// Set the number of lives for the player
        /// </summary>
        /// <param name="lives">Defaults to 2. Pass in optional integer for other values</param>
        public void SetLives(int lives = 2)
        {
            Lives = lives;
        }

        public void SetPlayerName(string playername)
        {
            Name = string.IsNullOrWhiteSpace(playername) ? "Player One" : playername;
        }

        #endregion
    }
}
