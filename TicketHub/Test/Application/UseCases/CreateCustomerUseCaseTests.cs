using Application.UseCases.CreateCustomer;
using Domain.Customers;

namespace Test.Application.UseCases
{
    public class CreateCustomerUseCaseTests
    {
        [Fact]
        public void Execute_WhenCalled_MustCreateCustomer()
        {
            // Arrange
            CreateCustomerInput createInput = new CreateCustomerInput(
                new Name("John Doe"),
                new Email("john.doe@gmail.com"),
                new Cpf("12345678901"));

            // Act
            CreateCustomerUseCase useCase = new CreateCustomerUseCase();
            CreateCustomerOutput output = useCase.Execute(createInput);

            // Assert
            Assert.NotEqual(Guid.Empty, output.Id);
            Assert.Equal(new Cpf("12345678901"), output.Cpf);
            Assert.Equal(new Email("john.doe@gmail.com"), output.Email);
            Assert.Equal(new Name("John Doe"), output.Name);
        }
    }
}
