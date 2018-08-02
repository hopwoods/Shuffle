using NUnit.Framework;
using Shuffle.Logic;
using Shuffle.Model;

namespace Shuffle.Tests
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