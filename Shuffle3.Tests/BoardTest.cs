using Shuffle.Model;
using NUnit.Framework;

namespace Shuffle.Test
{
    [TestFixture]
    public class BoardTests
    {
        [Test]
        public void CellsHaveCorrectNumberOfColumns()
        {
            //Arrange
            Board result = new Board();
            //Act

            //Assert
            Assert.That(result.Cells.GetLength(0), Is.EqualTo(8));
        }

        [Test]
        public void CellsHaveCorrectNumberOfRows()
        {
            //Arrange
            Board result = new Board();
            //Act

            //Assert
            Assert.That(result.Cells.GetLength(1), Is.EqualTo(8));
        }

        [Test]
        public void BoardDrawsCorrectly()
        {
            //Act
            Board board = new Board();
            //Arrange
            bool result = board.DrawBoard();
            //Assert
            Assert.That(result, Is.True);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void FormatCellValueGivenCellValue(int cellValue)
        {
            //Arrange
            Board board = new Board();
            //Act
            int result = board.FormatCell(cellValue);
            //Assert
            Assert.That(result, Is.EqualTo(cellValue));
        }
    }
}