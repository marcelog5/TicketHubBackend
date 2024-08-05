using Domain.Partners;
using Domain.Shared;

namespace Test.Domain
{
    public class PartnerTests
    {
        [Fact]
        public void Create_WhenCalled_CreatesPartner()
        {
            // Arrange
            var name = new Name("John Doe");
            var email = new Email("john.doe@gmail.com");
            var cnpj = new Cnpj("19.764.730/0001-97");

            // Act
            var partner = Partner.Create(name, email, cnpj);

            // Assert
            Assert.NotNull(partner);
            Assert.Equal(name, partner.Name);
            Assert.Equal(email, partner.Email);
            Assert.Equal(cnpj, partner.Cnpj);
        }
    }
}
