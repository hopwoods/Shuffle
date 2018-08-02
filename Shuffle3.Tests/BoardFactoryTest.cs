using NUnit.Framework;
using Shuffle3.Logic;
using Shuffle3.Model;

namespace Shuffle3.Tests
{
    [TestFixture]
    public class BoardFactoryTest
    {
        [Test]
        public void GetBoardReturnsBoard()
        {
            //Arrange
            BoardFactory boardFactory = new BoardFactory();
            //Act
            Board board = boardFactory.Get();
            //Assert
            Assert.That(board, Is.TypeOf(typeof(Board)));
        }
    }
}