using Application.UseCases.TicketUseCases;
using Domain.Abstracts;
using Domain.Customers;
using Domain.Events;
using Domain.Shared;
using Domain.Tickets;
using Moq;

namespace Test.Application.UseCases.TicketsTests
{
    public class SubscribeCustomerToEventUseCaseTests
    {
        [Fact]
        public async void Execute_WhenCalled_MustSubscribeCustomerToEvent()
        {
            // Arrange
            Guid customerId = Guid.NewGuid();
            Guid eventId = Guid.NewGuid();
            Guid TicketId = Guid.NewGuid();

            Customer customer = new Customer(
                customerId,
                new Name("John Doe"),
                new Email("john.doe@gmail.com"),
                new Cpf("12345678901"));

            Event @event = new Event(
                eventId,
                DateTime.Now,
                new Name("Event Name"),
                200,
                customerId);

            Ticket ticket = new Ticket(
                TicketId,
                EnTicketStatus.Pending,
                null,
                DateTime.UtcNow,
                customerId,
                eventId);

            SubscribeCustomerToEventInput createInput = new SubscribeCustomerToEventInput(
                eventId,
                customerId);

            var customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock
                .Setup(x => x.GetById(
                    customerId,
                    new CancellationToken()))
                .ReturnsAsync(customer);

            var eventRepoMock = new Mock<IEventRepository>();
            eventRepoMock.Setup(x => x.GetById(
                    eventId,
                    new CancellationToken()))
                .ReturnsAsync(@event);

            var ticketRepoMock = new Mock<ITicketRepository>();
            ticketRepoMock
                .Setup(x => x.Add(
                    ticket,
                    new CancellationToken()));

            SubscribeCustomerToEventUseCase useCase = new SubscribeCustomerToEventUseCase(
                customerRepoMock.Object,
                eventRepoMock.Object,
                ticketRepoMock.Object);

            // Act
            Result<SubscribeCustomerToEventOutput> output = await useCase.Execute(
                createInput,
                new CancellationToken());

            // Assert
            Assert.NotEqual(Guid.Empty, output.Value.TicketId);
            Assert.Equal(EnTicketStatus.Pending, output.Value.Status);
        }

        [Fact]
        public async void Execute_WhenCalled_WithInvalidCustomerId_MustReturnError()
        {
            // Arrange
            Guid customerId = Guid.NewGuid();
            Guid eventId = Guid.NewGuid();

            Customer customer = null;

            SubscribeCustomerToEventInput createInput = new SubscribeCustomerToEventInput(
                eventId,
                customerId);

            var customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock
                .Setup(x => x.GetById(
                    customerId,
                    new CancellationToken()))
                .ReturnsAsync(customer);

            var eventRepoMock = new Mock<IEventRepository>();
            var ticketRepoMock = new Mock<ITicketRepository>();

            SubscribeCustomerToEventUseCase useCase = new SubscribeCustomerToEventUseCase(
                customerRepoMock.Object,
                eventRepoMock.Object,
                ticketRepoMock.Object);

            // Act
            Result<SubscribeCustomerToEventOutput> output = await useCase.Execute(
                createInput,
                new CancellationToken());

            // Assert
            Assert.Equal(output.Error, CustomerErrors.NotFound);
        }

        [Fact]
        public async void Execute_WhenCalled_WithInvalidEventId_MustReturnError()
        {
            // Arrange
            Guid customerId = Guid.NewGuid();
            Guid eventId = Guid.NewGuid();

            Customer customer = new Customer(
                customerId,
                new Name("John Doe"),
                new Email("john.doe@gmail.com"),
                new Cpf("12345678901"));

            Event @event = null;

            SubscribeCustomerToEventInput createInput = new SubscribeCustomerToEventInput(
                eventId,
                customerId);

            var customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock
                .Setup(x => x.GetById(
                    customerId,
                    new CancellationToken()))
                .ReturnsAsync(customer);

            var eventRepoMock = new Mock<IEventRepository>();
            eventRepoMock.Setup(x => x.GetById(
                    eventId,
                    new CancellationToken()))
                .ReturnsAsync(@event);

            var ticketRepoMock = new Mock<ITicketRepository>();

            SubscribeCustomerToEventUseCase useCase = new SubscribeCustomerToEventUseCase(
                customerRepoMock.Object,
                eventRepoMock.Object,
                ticketRepoMock.Object);

            // Act
            Result<SubscribeCustomerToEventOutput> output = await useCase.Execute(
                createInput,
                new CancellationToken());

            // Assert
            Assert.Equal(output.Error, EventErrors.NotFound);
        }

        [Fact]
        public async void Execute_WhenCalled_WithMoreTicketsThanSpots_MustReturnError()
        {
            // Arrange
            Guid customerId = Guid.NewGuid();
            Guid eventId = Guid.NewGuid();
            int spotsNumber = 10;

            Customer customer = new Customer(
                customerId,
                new Name("John Doe"),
                new Email("john.doe@gmail.com"),
                new Cpf("12345678901"));

            Event @event = new Event(
                eventId,
                DateTime.Now,
                new Name("Event Name"),
                spotsNumber,
                customerId);

            for (int i = 0; i < spotsNumber + 1; i++)
            {
                @event.Tickets.Add(new Ticket(
                    Guid.NewGuid(),
                    EnTicketStatus.Pending,
                    null,
                    DateTime.UtcNow,
                    customerId,
                    eventId));
            }

            SubscribeCustomerToEventInput createInput = new SubscribeCustomerToEventInput(
                eventId,
                customerId);

            var customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock
                .Setup(x => x.GetById(
                    customerId,
                    new CancellationToken()))
                .ReturnsAsync(customer);

            var eventRepoMock = new Mock<IEventRepository>();
            eventRepoMock.Setup(x => x.GetById(
                    eventId,
                    new CancellationToken()))
                .ReturnsAsync(@event);

            var ticketRepoMock = new Mock<ITicketRepository>();

            SubscribeCustomerToEventUseCase useCase = new SubscribeCustomerToEventUseCase(
                customerRepoMock.Object,
                eventRepoMock.Object,
                ticketRepoMock.Object);

            // Act
            Result<SubscribeCustomerToEventOutput> output = await useCase.Execute(
                createInput,
                new CancellationToken());

            // Assert
            Assert.Equal(output.Error, EventErrors.NotEnoughSpots);
        }
    }
}
