using Domain.Events;
using Domain.Shared;
using Domain.Tickets;

namespace Test.Domain
{
    public class EventTests
    {
        [Fact]
        public void Create_WhenCalledWithValidParameters_CreatesEvent()
        {
            // Arrange
            var date = DateTime.Now.AddDays(1); // Data no futuro
            var name = new Name("Concert");
            var totalSpots = 100;
            var partnerId = Guid.NewGuid();

            // Act
            var eventObj = Event.Create(date, name, totalSpots, partnerId);

            // Assert
            Assert.NotNull(eventObj);
            Assert.Equal(date, eventObj.Date);
            Assert.Equal(name, eventObj.Name);
            Assert.Equal(totalSpots, eventObj.TotalSpots);
            Assert.Equal(partnerId, eventObj.PartnerId);
        }

        [Fact]
        public void Create_WhenCalledWithPastDate_ThrowsException()
        {
            // Arrange
            var pastDate = DateTime.Now.AddDays(-1); // Data no passado
            var name = new Name("Concert");
            var totalSpots = 100;
            var partnerId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<Exception>(() => Event.Create(pastDate, name, totalSpots, partnerId));
        }

        [Fact]
        public void Create_WhenCalledWithNonPositiveTotalSpots_ThrowsException()
        {
            // Arrange
            var date = DateTime.Now.AddDays(1);
            var name = new Name("Concert");
            var invalidTotalSpots = 0; // Total de vagas inválido
            var partnerId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<Exception>(() => Event.Create(date, name, invalidTotalSpots, partnerId));
        }

        [Fact]
        public void Create_WhenCalledWithEmptyPartnerId_ThrowsException()
        {
            // Arrange
            var date = DateTime.Now.AddDays(1);
            var name = new Name("Concert");
            var totalSpots = 100;
            var emptyPartnerId = Guid.Empty; // PartnerId inválido

            // Act & Assert
            Assert.Throws<Exception>(() => Event.Create(date, name, totalSpots, emptyPartnerId));
        }

        [Fact]
        public void ReserveTicket_WhenCustomerAlreadyHasTicket_ReturnsFailureResult()
        {
            // Arrange
            var date = DateTime.Now.AddDays(1);
            var name = new Name("Concert");
            var totalSpots = 100;
            var partnerId = Guid.NewGuid();
            var eventObj = Event.Create(date, name, totalSpots, partnerId);
            var customerId = Guid.NewGuid();

            // Reserve a ticket first
            eventObj.ReserveTicket(customerId);

            // Act
            var result = eventObj.ReserveTicket(customerId);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(TicketErrors.CustomerAlreadySubscribed, result.Error);
        }

        [Fact]
        public void ReserveTicket_WhenNoEnoughSpots_ReturnsFailureResult()
        {
            // Arrange
            var date = DateTime.Now.AddDays(1);
            var name = new Name("Concert");
            var totalSpots = 1; // Apenas uma vaga
            var partnerId = Guid.NewGuid();
            var eventObj = Event.Create(date, name, totalSpots, partnerId);
            var customerId = Guid.NewGuid();

            // Reserve the only available ticket
            eventObj.ReserveTicket(customerId);

            // Act
            var result = eventObj.ReserveTicket(Guid.NewGuid()); // Tentativa de reserva sem vagas

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(EventErrors.NotEnoughSpots, result.Error);
        }

        [Fact]
        public void ReserveTicket_WhenCalledSuccessfully_ReservesTicket()
        {
            // Arrange
            var date = DateTime.Now.AddDays(1);
            var name = new Name("Concert");
            var totalSpots = 100;
            var partnerId = Guid.NewGuid();
            var eventObj = Event.Create(date, name, totalSpots, partnerId);
            var customerId = Guid.NewGuid();

            // Act
            var result = eventObj.ReserveTicket(customerId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(customerId, result.Value.CustomerId);
            Assert.Equal(eventObj.Id, result.Value.EventId);
        }
    }
}
