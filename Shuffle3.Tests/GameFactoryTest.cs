using NUnit.Framework;
using Shuffle.Logic;
using Shuffle.Model;

namespace Shuffle.Tests
{
    [TestFixture]
    public class GameFactoryTest
    {
        [Test]
        public void GameFactoryReturnsGame()
        {
            //Arrange
            UserInterface userInterface = new UserInterface();
            BoardFactory boardFactory = new BoardFactory();
            //Act
            GameFactory gameFactory = new GameFactory(userInterface, boardFactory);
            //Asset
            Assert.That(gameFactory, Is.TypeOf<GameFactory>());
        }
    }
}