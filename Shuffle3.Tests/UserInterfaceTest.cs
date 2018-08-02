using NUnit.Framework;
using Shuffle.Model;

namespace Shuffle.Tests
{
    [TestFixture]
    public class UserInterfaceTest
    {
        [Test]
        public void ValidateMoveShouldEqualUp()
        {
            //Arrange
            UserInterface userInterface = new UserInterface();

            //Act
            int result = userInterface.ValidateMove("U");

            //Assert
            Assert.That(result, Is.EqualTo((int)Direction.Up));
        }
        [Test]
        public void ValidateMoveShouldEqualDown()
        {
            //Arrange
            UserInterface userInterface = new UserInterface();

            //Act
            int result = userInterface.ValidateMove("D");

            //Assert
            Assert.That(result, Is.EqualTo((int)Direction.Down));
        }
        [Test]
        public void ValidateMoveShouldEqualLeft()
        {
            //Arrange
            UserInterface userInterface = new UserInterface();

            //Act
            int result = userInterface.ValidateMove("L");

            //Assert
            Assert.That(result, Is.EqualTo((int)Direction.Left));
        }
        [Test]
        public void ValidateMoveShouldEqualRight()
        {
            //Arrange
            UserInterface userInterface = new UserInterface();

            //Act
            int result = userInterface.ValidateMove("R");

            //Assert
            Assert.That(result, Is.EqualTo((int)Direction.Right));
        }
        [Test]
        public void ValidateMoveShouldEqualInvalid()
        {
            //Arrange
            UserInterface userInterface = new UserInterface();

            //Act
            int result = userInterface.ValidateMove("A");

            //Assert
            Assert.That(result, Is.EqualTo((int)Direction.Invalid));
        }
    }
}
