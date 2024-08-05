using Application.UseCases.CustomerUseCases.GetCustomer;
using Domain.Abstracts;
using Domain.Customers;
using Domain.Shared;
using Moq;

namespace Test.Application.UseCases.CustomerTests
{
    public class GetCustomerByIdUseCaseTests
    {
        [Fact]
        public async void Execute_WhenCalled_MustGetCustomer()
        {
            // Arrange
            Customer customer = Customer.Create(
                new Name("John Doe"),
                new Email("john.doe@gmail.com"),
                new Cpf("959.021.760-50"));

            GetCustomerByIdInput createInput = new GetCustomerByIdInput(
                customer.Id);

            var customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock
                .Setup(x => x.GetById(
                    customer.Id,
                    new CancellationToken()))
                .ReturnsAsync(customer);

            GetCustomerByIdUseCase useCase = new GetCustomerByIdUseCase(customerRepoMock.Object);

            // Act
            Result<GetCustomerByIdOutput> output = await useCase.Execute(
                createInput,
                new CancellationToken());

            // Assert
            Assert.Equal(new Name("John Doe"), output.Value.Name);
            Assert.Equal(new Email("john.doe@gmail.com"), output.Value.Email);
            Assert.Equal(new Cpf("959.021.760-50"), output.Value.Cpf);
        }

        [Fact]
        public async Task Execute_WhenCustomerNotFound_MustReturnResultError()
        {
            // Arrange
            Customer customer = null;
            Guid invalidId = Guid.NewGuid();

            GetCustomerByIdInput createInput = new GetCustomerByIdInput(
                invalidId);

            var customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock
                .Setup(x => x.GetById(
                    invalidId,
                    new CancellationToken()))
                .ReturnsAsync(customer);

            GetCustomerByIdUseCase useCase = new GetCustomerByIdUseCase(customerRepoMock.Object);

            // Act
            Result<GetCustomerByIdOutput> output = await useCase.Execute(
                createInput,
                new CancellationToken());

            // Assert
            Assert.Equal(output.Error, CustomerErrors.NotFound);
        }
    }
}
