using Domain.Customers;

namespace Test.Domain
{
    public class CpfTests
    {
        [Fact]
        public void Create_WhenCalledWithValidCpfWithMask_CreatesCpf()
        {
            // Arrange
            var validCpfWithMask = "959.021.760-50";

            // Act
            var cpf = new Cpf(validCpfWithMask);

            // Assert
            Assert.NotNull(cpf);
            Assert.Equal("95902176050", cpf.Value);
        }

        [Fact]
        public void Create_WhenCalledWithValidCpfWithoutMask_CreatesCpf()
        {
            // Arrange
            var validCpfWithoutMask = "95902176050";

            // Act
            var cpf = new Cpf(validCpfWithoutMask);

            // Assert
            Assert.NotNull(cpf);
            Assert.Equal("95902176050", cpf.Value);
        }

        [Fact]
        public void Create_WhenCalledWithInvalidCpfWithMask_ThrowsArgumentException()
        {
            // Arrange
            var invalidCpfWithMask = "123.456.789-00";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Cpf(invalidCpfWithMask));
        }

        [Fact]
        public void Create_WhenCalledWithInvalidCpfWithoutMask_ThrowsArgumentException()
        {
            // Arrange
            var invalidCpfWithoutMask = "12345678900";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Cpf(invalidCpfWithoutMask));
        }

        [Fact]
        public void Create_WhenCalledWithCpfWithLessThan11Digits_ThrowsArgumentException()
        {
            // Arrange
            var shortCpf = "123.456.789";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Cpf(shortCpf));
        }

        [Fact]
        public void Create_WhenCalledWithCpfWithMoreThan11Digits_ThrowsArgumentException()
        {
            // Arrange
            var longCpf = "123.456.789-012";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Cpf(longCpf));
        }

        [Fact]
        public void Create_WhenCalledWithCpfContainingNonNumericCharacters_ThrowsArgumentException()
        {
            // Arrange
            var cpfWithLetters = "123.456.ABC-00";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Cpf(cpfWithLetters));
        }
    }
}
