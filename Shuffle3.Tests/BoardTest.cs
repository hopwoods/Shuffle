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
        [TestCase(CellStatus.PlayerIsHit)]
        public void SetCell_ShouldSetCorrectValueGivenStatus(CellStatus cellStatus)
        {
            //Arrange
            Board board = new Board();
            //Act
            board.SetCell(4,4,cellStatus);
            //Assert
            Assert.That(board.Cells[4,4], Is.EqualTo((int) cellStatus));
        }


        [TestCase(CellStatus.HiddenMine)]
        [TestCase(CellStatus.Empty)]
        [TestCase(CellStatus.Player)]
        [TestCase(CellStatus.Mine)]
        [TestCase(CellStatus.PlayerIsHit)]
        public void SetCell_ProvidedWithPositionShouldSetCorrectValueGivenStatus(CellStatus cellStatus)
        {
            //Arrange
            Board board = new Board();
            Position position = new Position(4,4);
            //Act
            board.SetCell(position,cellStatus);
            //Assert
            Assert.That(board.Cells[4,4], Is.EqualTo((int) cellStatus));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void FormatCell_GivenCellValue(int cellValue)
        {
            //Arrange
            Board board = new Board();
            //Act
            int result = board.FormatCell(cellValue);
            //Assert
            Assert.That(result, Is.EqualTo(cellValue));
        }

        [Test]
        public void SetCurrentPlayerPosition_ShouldUpdatePlayerPositionXValue()
        {
            //Arrange
            Board board = new Board();
            Position playerPosition = new Position(4,4);
            //Act
            board.SetCurrentPlayerPosition(playerPosition);
            //Assert
            Assert.That(board.PlayerPosition.X, Is.EqualTo(4));
        }
        [Test]
        public void SetCurrentPlayerPosition_ShouldUpdatePlayerPositionYValue()
        {
            //Arrange
            Board board = new Board();
            Position playerPosition = new Position(4,4);
            //Act
            board.SetCurrentPlayerPosition(playerPosition);
            //Assert
            Assert.That(board.PlayerPosition.X, Is.EqualTo(4));
        }

        [TestCase(Direction.Up)]
        [TestCase(Direction.Down)]
        [TestCase(Direction.Left)]
        [TestCase(Direction.Right)]
        public void MovePlayer_SetsCurrentPlayerPositionToEmpty(Direction direction)
        {
            //Arrange
            Board board = new Board();
            Position playerPosition = new Position(4,4);
            board.SetCurrentPlayerPosition(playerPosition);
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
        public void MovePlayer_SetsNewPlayerPositionToPlayer(Direction direction)
        {
            //Arrange
            Board board = new Board();
            Position playerPosition = new Position(4,4);
            board.SetCurrentPlayerPosition(playerPosition);
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
            board.SetCell(newPlayerPosition, CellStatus.Empty);
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
        public void MovePlayer_ShouldUpdatePlayerPositionXValue(Direction direction)
        {
            //Arrange
            Board board = new Board();
            Position playerPosition = new Position(4,4);
            board.SetCurrentPlayerPosition(playerPosition);
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
        public void MovePlayer_ShouldUpdatePlayerPositionYValue(Direction direction)
        {
            //Arrange
            Board board = new Board();
            Position playerPosition = new Position(4,4);
            board.SetCurrentPlayerPosition(playerPosition);
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
        [TestCase(Direction.Up)]
        [TestCase(Direction.Down)]
        [TestCase(Direction.Left)]
        [TestCase(Direction.Right)]
        public void MovePlayer_ShouldSetMoveMessage(Direction direction)
        {
            //Arrange
            Board board = new Board();
            Position playerPosition = null;
            switch (direction)
            {
                case Direction.Up:
                    playerPosition = new Position(0,0);
                    break;
                case Direction.Down:
                    playerPosition = new Position(7,0);
                    break;
                case Direction.Left:
                    playerPosition = new Position(0,0);
                    break;
                case Direction.Right:
                    playerPosition = new Position(0,7);
                    break;   
            }
            board.SetCurrentPlayerPosition(playerPosition);

            //Act
            string moveMessage = board.MovePlayer(direction);
            //Assert
            Assert.That(moveMessage, Contains.Substring("You can\'t move"));
            
        }
       
        [Test]
        public void ClearCell_ShouldSetGivenCellValueToEmpty()
        {
            //Arrange
            Board board = new Board();
            Position postion = new Position(4,4);
            board.SetCell(postion, CellStatus.Player);
            //Act
            board.ClearCell(postion);
            //Assert
            Assert.That(board.Cells[4,4], Is.EqualTo((int) CellStatus.Empty));
        }
       
        [Test]
        public void ClearCell_ShouldSetGivenCellValueToMine()
        {
            //Arrange
            Board board = new Board();
            Position postion = new Position(4,4);
            board.SetCell(postion, CellStatus.PlayerIsHit);
            //Act
            board.ClearCell(postion);
            //Assert
            Assert.That(board.Cells[4,4], Is.EqualTo((int) CellStatus.Mine));
        }
       
        [Test]
        public void MoveToCell_ShouldSetGivenCellValueToPlayer()
        {
            //Arrange
            Board board = new Board();
            Position postion = new Position(4,4);
            //Act
            board.MoveToCell(postion);
            //Assert
            Assert.That(board.Cells[4,4], Is.EqualTo((int) CellStatus.Player));
        }

        [Test]
        public void IsCellMined_ReturnsTrue()
        {
            //Arrange
            Board board = new Board();
            Position position = new Position(4,4);
            board.SetCell(4,4,CellStatus.HiddenMine);
            //Act
            bool result = board.IsCellMined(position);
            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsCellMined_ReturnsFalse()
        {
            //Arrange
            Board board = new Board();
            Position position = new Position(4,4);
            //Act
            bool result = board.IsCellMined(position);
            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CellStatus_ReturnsCorrectStatus()
        {
            //Arrange
            Board board = new Board();
            Position position = new Position(4,4);
            board.SetCell(position,CellStatus.Player);
            //Act
            int result = board.GetCellStatus(position);
            //Assert
            Assert.That(result, Is.EqualTo((int) CellStatus.Player));
        }

        [Test]
        public void ExplodeMine()
        {
            //Arrange
            Board board = new Board();
            Position position = new Position(4,4);
            board.SetCell(position,CellStatus.HiddenMine);
            //Act
            board.Explode(position);
            //Assert
            Assert.That(board.Cells[4,4], Is.EqualTo((int)CellStatus.PlayerIsHit));
        } 

        [Test]
        public void IsCellInTopRow_IsTrue()
        {
            //Arrange
            Board board = new Board();
            Position position = new Position(0,4);
            //Act
            bool result = board.IsCellInTopRow(position);
            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsCellInTopRow_IsFalse()
        {
            //Arrange
            Board board = new Board();
            Position position = new Position(0,4);
            //Act
            bool result = board.IsCellInTopRow(position);
            //Assert
            Assert.That(result, Is.True);
        }
    }
        

}