using Application.UseCases.CreateCustomer;
using Domain.Abstracts;
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

            var customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock
                .Setup(x => x.CustomerAlreadyExist(
                    new Email("john.doe@gmail.com"), 
                    new Cpf("12345678901"), 
                    new CancellationToken()))
                .ReturnsAsync(false);
            customerRepoMock.Setup(x => x.AddCustomer(It.IsAny<Customer>()));

            CreateCustomerUseCase useCase = new CreateCustomerUseCase(customerRepoMock.Object);

            // Act
            Result<CreateCustomerOutput> output = await useCase.Execute(createInput, new CancellationToken());

            // Assert
            Assert.NotEqual(Guid.Empty, output.Value.Id);
            Assert.Equal(new Name("John Doe"), output.Value.Name);
            Assert.Equal(new Email("john.doe@gmail.com"), output.Value.Email);
            Assert.Equal(new Cpf("12345678901"), output.Value.Cpf);
        }

        [Fact]
        public async Task Execute_WhenCustomerAlreadyExist_MustThrowException()
        {
            // Arrange
            CreateCustomerInput createInput = new CreateCustomerInput(
                new Name("John Doe"),
                new Email("john.doe@gmail.com"),
                new Cpf("12345678901"));

            var customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock
                .Setup(x => x.CustomerAlreadyExist(
                    new Email("john.doe@gmail.com"),
                    new Cpf("12345678901"),
                    It.IsAny<CancellationToken>())) // Use It.IsAny<CancellationToken>() para permitir qualquer token de cancelamento
                .ReturnsAsync(true);

            CreateCustomerUseCase useCase = new CreateCustomerUseCase(customerRepoMock.Object);

            // Act
            Result<CreateCustomerOutput> output = await useCase.Execute(createInput, new CancellationToken());

            // Assert
            Assert.Equal(output.Error, CustomerErrors.AlreadyExists);
        }

    }
}
