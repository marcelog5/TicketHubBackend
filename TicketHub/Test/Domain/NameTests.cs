using Domain.Shared;

namespace Test.Domain
{
    public class NameTests
    {
        [Fact]
        public void Create_WhenCalledWithValidName_CreatesName()
        {
            // Arrange
            var validName = "John Doe";

            // Act
            var name = new Name(validName);

            // Assert
            Assert.NotNull(name);
            Assert.Equal(validName, name.Value);
        }

        [Fact]
        public void Create_WhenCalledWithEmptyName_ThrowsArgumentException()
        {
            // Arrange
            var emptyName = "";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Name(emptyName));
        }

        [Fact]
        public void Create_WhenCalledWithWhitespaceName_ThrowsArgumentException()
        {
            // Arrange
            var whitespaceName = "     ";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Name(whitespaceName));
        }

        [Fact]
        public void Create_WhenCalledWithNullName_ThrowsArgumentException()
        {
            // Arrange
            string nullName = null;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Name(nullName));
        }
    }
}
