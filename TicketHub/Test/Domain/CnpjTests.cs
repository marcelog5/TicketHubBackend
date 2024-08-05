using Domain.Partners;

namespace Test.Domain
{
    public class CnpjTests
    {
        [Fact]
        public void Create_WhenCalledWithValidCnpjWithMask_CreatesCnpj()
        {
            // Arrange
            var validCnpjWithMask = "12.345.678/0001-95";

            // Act
            var cnpj = new Cnpj(validCnpjWithMask);

            // Assert
            Assert.NotNull(cnpj);
            Assert.Equal("12345678000195", cnpj.Value);
        }

        [Fact]
        public void Create_WhenCalledWithValidCnpjWithoutMask_CreatesCnpj()
        {
            // Arrange
            var validCnpjWithoutMask = "12345678000195";

            // Act
            var cnpj = new Cnpj(validCnpjWithoutMask);

            // Assert
            Assert.NotNull(cnpj);
            Assert.Equal("12345678000195", cnpj.Value);
        }

        [Fact]
        public void Create_WhenCalledWithInvalidCnpjWithMask_ThrowsArgumentException()
        {
            // Arrange
            var invalidCnpjWithMask = "12.345.678/0001-00";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Cnpj(invalidCnpjWithMask));
        }

        [Fact]
        public void Create_WhenCalledWithInvalidCnpjWithoutMask_ThrowsArgumentException()
        {
            // Arrange
            var invalidCnpjWithoutMask = "12345678000100";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Cnpj(invalidCnpjWithoutMask));
        }

        [Fact]
        public void Create_WhenCalledWithCnpjWithLessThan14Digits_ThrowsArgumentException()
        {
            // Arrange
            var shortCnpj = "12.345.678/0001-9";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Cnpj(shortCnpj));
        }

        [Fact]
        public void Create_WhenCalledWithCnpjWithMoreThan14Digits_ThrowsArgumentException()
        {
            // Arrange
            var longCnpj = "12.345.678/0001-999";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Cnpj(longCnpj));
        }

        [Fact]
        public void Create_WhenCalledWithCnpjContainingNonNumericCharacters_ThrowsArgumentException()
        {
            // Arrange
            var cnpjWithLetters = "12.34A.678/0001-95";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Cnpj(cnpjWithLetters));
        }
    }
}
