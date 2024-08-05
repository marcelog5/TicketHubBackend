using Domain.Shared;

namespace Test.Domain
{
    public class EmailTests
    {
        [Fact]
        public void Create_WhenCalledWithValidEmail_CreatesEmail()
        {
            // Arrange
            var validEmail = "john.doe@gmail.com";

            // Act
            var email = new Email(validEmail);

            // Assert
            Assert.NotNull(email);
            Assert.Equal(validEmail, email.Value);
        }

        [Fact]
        public void Create_WhenCalledWithValidEmailWithUppercase_CreatesEmail()
        {
            // Arrange
            var validEmailUppercase = "JOHN.DOE@GMAIL.COM";

            // Act
            var email = new Email(validEmailUppercase);

            // Assert
            Assert.NotNull(email);
            Assert.Equal(validEmailUppercase.ToLower(), email.Value);
        }

        [Fact]
        public void Create_WhenCalledWithInvalidEmailWithoutAtSymbol_ThrowsArgumentException()
        {
            // Arrange
            var invalidEmail = "john.doe.gmail.com";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Email(invalidEmail));
        }

        [Fact]
        public void Create_WhenCalledWithInvalidEmailWithoutDomain_ThrowsArgumentException()
        {
            // Arrange
            var invalidEmail = "john.doe@.com";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Email(invalidEmail));
        }

        [Fact]
        public void Create_WhenCalledWithEmailContainingSpaces_ThrowsArgumentException()
        {
            // Arrange
            var emailWithSpaces = "john.doe @gmail.com";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Email(emailWithSpaces));
        }

        [Fact]
        public void Create_WhenCalledWithEmptyEmail_ThrowsArgumentException()
        {
            // Arrange
            var emptyEmail = "";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Email(emptyEmail));
        }

        [Fact]
        public void Create_WhenCalledWithNullEmail_ThrowsArgumentException()
        {
            // Arrange
            string nullEmail = null;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Email(nullEmail));
        }
    }
}
