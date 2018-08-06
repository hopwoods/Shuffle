using NUnit.Framework;
using Shuffle.Logic;
using Shuffle.Model;

namespace Shuffle.Tests
{
    [TestFixture]
    public class PlayerTest
    {
        private readonly PlayerFactory _playerFactory = new PlayerFactory();

        [Test]
        public void SetLivesWithoutValueDefaultsto2()
        {
            //Arrange
            Player player = _playerFactory.CreatePlayer();
            //Act
            player.SetLives();
            //Assert
            Assert.That(player.Lives, Is.EqualTo(2));
        }

        [Test]
        public void SetLivesWithValueShouldSetCorrectNumberOfLives()
        {
            //Arrange
            Player player = _playerFactory.CreatePlayer();
            //Act
            player.SetLives(3);
            //Assert
            Assert.That(player.Lives, Is.EqualTo(3));
        }

        [Test]
        public void SetPlayerNameGivenNullOrEmptySetsPlayerNameToPlayerName()
        {
            //Arrange
            Player player = _playerFactory.CreatePlayer();
            //Act
            player.SetPlayerName("");
            //Assert
            Assert.That(player.Name, Is.EqualTo("Player One"));
        }

        [Test]
        public void SetPlayerNameGivenValueCorrectlySetsPlayerName()
        {
            //Arrange
            Player player = _playerFactory.CreatePlayer();
            //Act
            player.SetPlayerName("Stuart Hopwood");
            //Assert
            Assert.That(player.Name, Is.EqualTo("Stuart Hopwood"));
        }

        [Test]
        public void IsPlayerAliveReturnsTrueGivenLivesOf1()
        {
            //Arrange
            Player player = _playerFactory.CreatePlayer();
            player.SetLives(1);
            //Act
            bool isAlive = player.IsPlayerAlive();
            //Assert
            Assert.That(isAlive, Is.True);
        }

        [Test]
        public void IsPlayerAliveReturnsFalseGivenLivesOf0()
        {
            //Arrange
            Player player = _playerFactory.CreatePlayer();
            player.SetLives(0);
            //Act
            bool isAlive = player.IsPlayerAlive();
            //Assert
            Assert.That(isAlive, Is.False);
        }

        [TestCase(1)]
        [TestCase(2)]
        public void LoseLife(int numberOfLives)
        {
            //Arrange
            Player player = _playerFactory.CreatePlayer();
            player.SetLives(numberOfLives);
            int originalLives = player.Lives;
            //Act
            player.LoseLife();
            int livesLeft = player.Lives;
            //Assert
            Assert.That(livesLeft, Is.EqualTo(originalLives-1));
        }
    }
}