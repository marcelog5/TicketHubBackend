using Domain.Tickets;

namespace Test.Domain
{
    public class TicketTests
    {
        [Fact]
        public void Create_WhenCalledWithValidParameters_CreatesTicket()
        {
            // Arrange
            var status = EnTicketStatus.Pending;
            var paidAt = (DateTime?)null;
            var reservedAt = DateTime.UtcNow;
            var ordering = 1;
            var customerId = Guid.NewGuid();
            var eventId = Guid.NewGuid();

            // Act
            var ticket = Ticket.Create(
                status,
                paidAt,
                reservedAt,
                ordering,
                customerId,
                eventId);

            // Assert
            Assert.NotNull(ticket);
            Assert.Equal(status, ticket.Status);
            Assert.Equal(paidAt, ticket.PaidAt);
            Assert.Equal(reservedAt, ticket.ReservedAt);
            Assert.Equal(customerId, ticket.CustomerId);
            Assert.Equal(eventId, ticket.EventId);
            Assert.Equal(ordering, ticket.Ordering);
        }

        [Fact]
        public void Create_WhenCalledWithDefaultEventId_ThrowsArgumentNullException()
        {
            // Arrange
            var status = EnTicketStatus.Pending;
            var paidAt = (DateTime?)null;
            var reservedAt = DateTime.UtcNow;
            var ordering = 1;
            var customerId = Guid.NewGuid();
            var eventId = Guid.Empty; // ID do evento inválido

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Ticket.Create(
                status,
                paidAt,
                reservedAt,
                ordering,
                customerId,
                eventId));
        }

        [Fact]
        public void Create_WhenCalledWithDefaultCustomerId_ThrowsArgumentNullException()
        {
            // Arrange
            var status = EnTicketStatus.Pending;
            var paidAt = (DateTime?)null;
            var reservedAt = DateTime.UtcNow;
            var ordering = 1;
            var customerId = Guid.Empty; // ID do cliente inválido
            var eventId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Ticket.Create(
                status,
                paidAt,
                reservedAt,
                ordering,
                customerId,
                eventId));
        }

        [Fact]
        public void Create_WhenCalledWithDefaultReservedAt_ThrowsArgumentNullException()
        {
            // Arrange
            var status = EnTicketStatus.Pending;
            var paidAt = (DateTime?)null;
            var reservedAt = default(DateTime); // Data de reserva inválida
            var ordering = 1;
            var customerId = Guid.NewGuid();
            var eventId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Ticket.Create(
                status,
                paidAt,
                reservedAt,
                ordering,
                customerId,
                eventId));
        }

        [Fact]
        public void Create_WhenCalledWithNonPositiveOrdering_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var status = EnTicketStatus.Pending;
            var paidAt = (DateTime?)null;
            var reservedAt = DateTime.UtcNow;
            var ordering = 0; // Número de ordem inválido
            var customerId = Guid.NewGuid();
            var eventId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Ticket.Create(
                status,
                paidAt,
                reservedAt,
                ordering,
                customerId,
                eventId));
        }
    }
}
