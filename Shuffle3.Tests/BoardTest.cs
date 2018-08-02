using NUnit.Framework;
using Shuffle.Model;

namespace Shuffle.Tests
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
        public void BoardHasEmptyCellsWhenCreated()
        {
            //Arrange
            Board result = new Board();
            //Act

            //Assert
            Assert.That(result.Cells, Has.Some.EqualTo((int) CellStatus.Empty));
        }
        [Test]
        public void BoardHasHiddenMineCellsWhenCreated()
        {
            //Arrange
            Board result = new Board();
            //Act

            //Assert
            Assert.That(result.Cells, Has.Some.EqualTo((int) CellStatus.HiddenMine));
        }
        [Test]
        public void PlayerIsPlacedInCorrectStartingPosistion()
        {
            //Arrange
            Board result = new Board();
            //Act

            //Assert
            Assert.That(result.Cells[7,0], Is.EqualTo((int) CellStatus.Player));
        }

        [Test]
        public void BoardDrawsCorrectly()
        {
            //Arrange
            Board board = new Board();
            //Act
            bool result = board.DrawBoard();
            //Assert
            Assert.That(result, Is.True);
        }

        [TestCase(CellStatus.HiddenMine)]
        [TestCase(CellStatus.Empty)]
        [TestCase(CellStatus.Player)]
        [TestCase(CellStatus.Mine)]
        public void SetCellShouldSetCorrectValueGivenStatus(CellStatus cellStatus)
        {
            //Arrange
            Board board = new Board();
            //Act
            board.SetCell(4,4,cellStatus);
            //Assert
            Assert.That(board.Cells[4,4], Is.EqualTo((int) cellStatus));
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

        [Test]
        public void SetCurrentPlayerPositionShouldUpdatePlayerPositionXValue()
        {
            //Arrange
            Board board = new Board();
            //Act
            board.SetCurrentPlayerPosition(4,4);
            //Assert
            Assert.That(board.PlayerPosition.X, Is.EqualTo(4));
        }
        [Test]
        public void SetCurrentPlayerPositionShouldUpdatePlayerPositionYValue()
        {
            //Arrange
            Board board = new Board();
            //Act
            board.SetCurrentPlayerPosition(4,4);
            //Assert
            Assert.That(board.PlayerPosition.X, Is.EqualTo(4));
        }
        
    }
}