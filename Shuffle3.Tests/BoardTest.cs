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

        [TestCase(Direction.Up)]
        [TestCase(Direction.Down)]
        [TestCase(Direction.Left)]
        [TestCase(Direction.Right)]
        public void MovePlayerSetsCurrentPlayerPositionToEmpty(Direction direction)
        {
            //Arrange
            Board board = new Board();
            board.SetCurrentPlayerPosition(4,4);
            Position currentPlayerPosition = board.PlayerPosition;
            //Act
            board.MovePlayer(direction);
            //Assert
            Assert.That(board.Cells[currentPlayerPosition.X,currentPlayerPosition.Y], Is.EqualTo((int) CellStatus.Empty));
        }

        [TestCase(Direction.Up)]
        [TestCase(Direction.Down)]
        [TestCase(Direction.Left)]
        [TestCase(Direction.Right)]
        public void MovePlayerSetsNewPlayerPositionToPlayer(Direction direction)
        {
            //Arrange
            Board board = new Board();
            board.SetCurrentPlayerPosition(4,4);
            Position newPlayerPosition = null;
            switch (direction)
            {
                case Direction.Up:
                    newPlayerPosition = new Position(3,4);
                    break;
                case Direction.Down:
                    newPlayerPosition = new Position(5,4);
                    break;
                case Direction.Left:
                    newPlayerPosition = new Position(4,3);
                    break;
                case Direction.Right:
                    newPlayerPosition = new Position(4,5);
                    break;   
            }
            //Act
            board.MovePlayer(direction);
            //Assert
            if (newPlayerPosition != null)
            {
                Assert.That(board.Cells[newPlayerPosition.X, newPlayerPosition.Y], Is.EqualTo((int) CellStatus.Player));
            }
        }

        [TestCase(Direction.Up)]
        [TestCase(Direction.Down)]
        [TestCase(Direction.Left)]
        [TestCase(Direction.Right)]
        public void MovePlayerShouldUpdatePlayerPositionXValue(Direction direction)
        {
            //Arrange
            Board board = new Board();
            board.SetCurrentPlayerPosition(4,4);
            Position newPlayerPosition = null;
            switch (direction)
            {
                case Direction.Up:
                    newPlayerPosition = new Position(3,4);
                    break;
                case Direction.Down:
                    newPlayerPosition = new Position(5,4);
                    break;
                case Direction.Left:
                    newPlayerPosition = new Position(4,3);
                    break;
                case Direction.Right:
                    newPlayerPosition = new Position(4,5);
                    break;   
            }
            //Act
            board.MovePlayer(direction);
            //Assert
            if (newPlayerPosition != null)
            {
                Assert.That(board.PlayerPosition.X, Is.EqualTo(newPlayerPosition.X));
            }
        }
        [TestCase(Direction.Up)]
        [TestCase(Direction.Down)]
        [TestCase(Direction.Left)]
        [TestCase(Direction.Right)]
        public void MovePlayerShouldUpdatePlayerPositionYValue(Direction direction)
        {
            //Arrange
            Board board = new Board();
            board.SetCurrentPlayerPosition(4,4);
            Position newPlayerPosition = null;
            switch (direction)
            {
                case Direction.Up:
                    newPlayerPosition = new Position(3,4);
                    break;
                case Direction.Down:
                    newPlayerPosition = new Position(5,4);
                    break;
                case Direction.Left:
                    newPlayerPosition = new Position(4,3);
                    break;
                case Direction.Right:
                    newPlayerPosition = new Position(4,5);
                    break;   
            }
            //Act
            board.MovePlayer(direction);
            //Assert
            if (newPlayerPosition != null)
            {
                Assert.That(board.PlayerPosition.Y, Is.EqualTo(newPlayerPosition.Y));
            }
        }

        
    }
}