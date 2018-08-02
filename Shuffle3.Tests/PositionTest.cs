using NUnit.Framework;
using Shuffle.Model;

namespace Shuffle.Test
{
    [TestFixture]
    public class PositionTest
    {
        [Test]
        public void PositionXValueIsSetCorrectly()
        {
            //Arrange
            
            //Act
            Position position = new Position(1,1);
            //Assert
            Assert.That(position.X, Is.EqualTo(1));
        }
        [Test]
        public void PositionYValueIsSetCorrectly()
        {
            //Arrange
            
            //Act
            Position position = new Position(1,1);
            //Assert
            Assert.That(position.Y, Is.EqualTo(1));
        }
    }
}