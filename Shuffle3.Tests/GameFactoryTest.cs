using NUnit.Framework;
using Shuffle.Logic;
using Shuffle.Model;
using Shuffle.Utilities;

namespace Shuffle.Tests
{
    [TestFixture]
    public class GameFactoryTest
    {
        [Test]
        public void GameFactoryReturnsGame()
        {
            //Arrange
            UserInterface userInterface = new UserInterface(new Utility());
            BoardFactory boardFactory = new BoardFactory();
            PlayerFactory playerFactory = new PlayerFactory();
            //Act
            GameFactory gameFactory = new GameFactory(userInterface, boardFactory, playerFactory);
            //Asset
            Assert.That(gameFactory, Is.TypeOf<GameFactory>());
        }
    }
}