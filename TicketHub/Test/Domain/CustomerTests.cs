using Domain.Customers;
using Domain.Shared;

namespace Test.Domain
{
    public class CustomerTests
    {
        [Fact]
        public void Create_WhenCalled_CreatesCustomer()
        {
            // Arrange
            var name = new Name("John Doe");
            var email = new Email("john.doe@gmail.com");
            var cpf = new Cpf("959.021.760-50");

            // Act
            var customer = Customer.Create(name, email, cpf);

            // Assert
            Assert.NotNull(customer);
            Assert.Equal(name, customer.Name);
            Assert.Equal(email, customer.Email);
            Assert.Equal(cpf, customer.Cpf);
        }
    }
}
