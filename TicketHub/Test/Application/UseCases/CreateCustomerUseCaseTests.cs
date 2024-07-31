using Application.UseCases.CreateCustomer;
using Domain.Customers;
using Moq;

namespace Test.Application.UseCases
{
    public class CreateCustomerUseCaseTests
    {
        [Fact]
        public async void Execute_WhenCalled_MustCreateCustomer()
        {
            // Arrange
            CreateCustomerInput createInput = new CreateCustomerInput(
                new Name("John Doe"),
                new Email("john.doe@gmail.com"),
                new Cpf("12345678901"));

            Customer customerResponse = null;

            var customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock
                .Setup(x => x.CustomerAlreadyExist(
                    new Email("john.doe@gmail.com"), 
                    new Cpf("12345678901"), 
                    new CancellationToken()))
                .ReturnsAsync(customerResponse);
            customerRepoMock.Setup(x => x.AddCustomer(It.IsAny<Customer>()));

            CreateCustomerUseCase useCase = new CreateCustomerUseCase(customerRepoMock.Object);

            // Act
            CreateCustomerOutput output = await useCase.Execute(createInput, new CancellationToken());

            // Assert
            Assert.NotEqual(Guid.Empty, output.Id);
            Assert.Equal(new Name("John Doe"), output.Name);
            Assert.Equal(new Email("john.doe@gmail.com"), output.Email);
            Assert.Equal(new Cpf("12345678901"), output.Cpf);
        }

        [Fact]
        public async Task Execute_WhenCustomerAlreadyExist_MustThrowException()
        {
            // Arrange
            CreateCustomerInput createInput = new CreateCustomerInput(
                new Name("John Doe"),
                new Email("john.doe@gmail.com"),
                new Cpf("12345678901"));

            Customer customerResponse = new Customer(
                Guid.NewGuid(),
                new Name("John Doe"),
                new Email("john.doe@gmail.com"),
                new Cpf("12345678901"));

            var customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock
                .Setup(x => x.CustomerAlreadyExist(
                    new Email("john.doe@gmail.com"),
                    new Cpf("12345678901"),
                    It.IsAny<CancellationToken>())) // Use It.IsAny<CancellationToken>() para permitir qualquer token de cancelamento
                .ReturnsAsync(customerResponse);

            CreateCustomerUseCase useCase = new CreateCustomerUseCase(customerRepoMock.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAnyAsync<Exception>(() =>
                useCase.Execute(createInput, CancellationToken.None));

            Assert.Equal("Customer already exist.", exception.Message);
        }

    }
}
