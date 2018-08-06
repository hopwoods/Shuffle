using System;

namespace Shuffle.Model
{
    public class Player : IPlayer
    {
        #region Fields

        public int Lives = 2;
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

        /// <summary>
        /// Check if player has any remaining lives.
        /// </summary>
        /// <returns>True or False</returns>
        public bool IsPlayerAlive()
        {
            return Lives > 0;
        }

        /// <summary>
        /// Subtract a life from the player
        /// </summary>
        public void LoseLife()
        {
            Lives = Lives - 1;
        }

        #endregion
    }
}
