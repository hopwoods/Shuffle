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
            GameProcessor gameProcessor = new GameProcessor(userInterface, boardFactory, playerFactory);
            //Asset
            Assert.That(gameProcessor, Is.TypeOf<GameProcessor>());
        }
    }
}