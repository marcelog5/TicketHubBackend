using Application.UseCases.CustomerUseCases.CreateCustomer;
using Domain.Abstracts;
using Domain.Customers;
using Domain.Shared;
using Moq;

namespace Test.Application.UseCases.CustomerTests
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
                new Cpf("959.021.760-50"));

            var customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock
                .Setup(x => x.AlreadyExist(
                    new Email("john.doe@gmail.com"),
                    new Cpf("959.021.760-50"),
                    new CancellationToken()))
                .ReturnsAsync(false);
            customerRepoMock.Setup(x => x.Add(It.IsAny<Customer>(), new CancellationToken()));

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(x => x.SaveChangesAsync(new CancellationToken()))
                .ReturnsAsync(1);

            CreateCustomerUseCase useCase = new CreateCustomerUseCase(
                customerRepoMock.Object,
                unitOfWorkMock.Object);

            // Act
            Result<CreateCustomerOutput> output = await useCase.Execute(createInput, new CancellationToken());

            // Assert
            Assert.NotEqual(Guid.Empty, output.Value.Id);
            Assert.Equal(new Name("John Doe"), output.Value.Name);
            Assert.Equal(new Email("john.doe@gmail.com"), output.Value.Email);
            Assert.Equal(new Cpf("959.021.760-50"), output.Value.Cpf);
        }

        [Fact]
        public async Task Execute_WhenCustomerAlreadyExist_MustReturnError()
        {
            // Arrange
            CreateCustomerInput createInput = new CreateCustomerInput(
                new Name("John Doe"),
                new Email("john.doe@gmail.com"),
                new Cpf("959.021.760-50"));

            var customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock
                .Setup(x => x.AlreadyExist(
                    new Email("john.doe@gmail.com"),
                    new Cpf("959.021.760-50"),
                    It.IsAny<CancellationToken>())) // Use It.IsAny<CancellationToken>() para permitir qualquer token de cancelamento
                .ReturnsAsync(true);

            var unitOfWorkMock = new Mock<IUnitOfWork>();

            CreateCustomerUseCase useCase = new CreateCustomerUseCase(
                customerRepoMock.Object, 
                unitOfWorkMock.Object);

            // Act
            Result<CreateCustomerOutput> output = await useCase.Execute(createInput, new CancellationToken());

            // Assert
            Assert.Equal(output.Error, CustomerErrors.AlreadyExist);
        }
    }
}
