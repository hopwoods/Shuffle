using NUnit.Framework;
using Shuffle.Utilities;

namespace Shuffle.Tests
{
    [TestFixture]
    public class UtilityTest
    {
        [Test]
        public void IsStringTooLong_ReturnsTrueIfGreaterThan30()
        {
            //Arrange
            Utility utility = new Utility();
            //Act
            bool result = utility.IsStringTooLong(30, "0123456789012345678901234567890123456789");
            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsStringTooLong_ReturnsFalseIfLessThan30()
        {
            //Arrange
            Utility utility = new Utility();
            //Act
            bool result = utility.IsStringTooLong(30, "123456789");
            //Assert
            Assert.That(result, Is.False);
        }
    }
}