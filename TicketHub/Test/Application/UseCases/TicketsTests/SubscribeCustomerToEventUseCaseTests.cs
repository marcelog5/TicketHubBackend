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
            Customer customer = Customer.Create(
                new Name("John Doe"),
                new Email("john.doe@gmail.com"),
                new Cpf("959.021.760-50"));
            Guid customerId = customer.Id;

            Event @event = Event.Create(
                DateTime.Today.AddDays(1),
                new Name("Event Name"),
                200,
                customerId);
            Guid eventId = Guid.NewGuid();

            Ticket ticket = Ticket.Create(
                EnTicketStatus.Pending,
                null,
                DateTime.UtcNow,
                1,
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

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(x => x.SaveChangesAsync(new CancellationToken()))
                .ReturnsAsync(1);

            SubscribeCustomerToEventUseCase useCase = new SubscribeCustomerToEventUseCase(
                customerRepoMock.Object,
                eventRepoMock.Object,
                ticketRepoMock.Object, 
                unitOfWorkMock.Object);

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
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            SubscribeCustomerToEventUseCase useCase = new SubscribeCustomerToEventUseCase(
                customerRepoMock.Object,
                eventRepoMock.Object,
                ticketRepoMock.Object, 
                unitOfWorkMock.Object);

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
            Guid eventId = Guid.NewGuid();

            Customer customer = Customer.Create(
                new Name("John Doe"),
                new Email("john.doe@gmail.com"),
                new Cpf("959.021.760-50"));
            Guid customerId = customer.Id;

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
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            SubscribeCustomerToEventUseCase useCase = new SubscribeCustomerToEventUseCase(
                customerRepoMock.Object,
                eventRepoMock.Object,
                ticketRepoMock.Object,
                unitOfWorkMock.Object);

            // Act
            Result<SubscribeCustomerToEventOutput> output = await useCase.Execute(
                createInput,
                new CancellationToken());

            // Assert
            Assert.Equal(output.Error, EventErrors.NotFound);
        }

        [Fact]
        public async void Execute_WhenCalled_CustomerAlreadyHasTickets_MustReturnError()
        {
            // Arrange
            Guid eventId = Guid.NewGuid();
            int spotsNumber = 10;

            Customer customer = Customer.Create(
                new Name("John Doe"),
                new Email("john.doe@gmail.com"),
                new Cpf("959.021.760-50"));
            Guid customerId = customer.Id;

            Event @event = Event.Create(
                DateTime.Today.AddDays(1),
                new Name("Event Name"),
                spotsNumber,
                customerId);

            @event.Tickets.Add(Ticket.Create(
                EnTicketStatus.Pending,
                null,
                DateTime.UtcNow,
                1,
                customerId,
                eventId));

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
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            SubscribeCustomerToEventUseCase useCase = new SubscribeCustomerToEventUseCase(
                customerRepoMock.Object,
                eventRepoMock.Object,
                ticketRepoMock.Object, 
                unitOfWorkMock.Object);

            // Act
            Result<SubscribeCustomerToEventOutput> output = await useCase.Execute(
                createInput,
                new CancellationToken());

            // Assert
            Assert.Equal(output.Error, TicketErrors.CustomerAlreadySubscribed);
        }

        [Fact]
        public async void Execute_WhenCalled_WithMoreTicketsThanSpots_MustReturnError()
        {
            // Arrange
            Guid eventId = Guid.NewGuid();
            int spotsNumber = 10;

            Customer customer = Customer.Create(
                new Name("John Doe"),
                new Email("john.doe@gmail.com"),
                new Cpf("959.021.760-50"));
            Guid customerId = customer.Id;

            Event @event = Event.Create(
                DateTime.Today.AddDays(1),
                new Name("Event Name"),
                spotsNumber,
                customerId);

            for (int i = 0; i < spotsNumber + 1; i++)
            {
                @event.Tickets.Add(Ticket.Create(
                    EnTicketStatus.Pending,
                    null,
                    DateTime.UtcNow,
                    1,
                    Guid.NewGuid(),
                    Guid.NewGuid()));
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
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            SubscribeCustomerToEventUseCase useCase = new SubscribeCustomerToEventUseCase(
                customerRepoMock.Object,
                eventRepoMock.Object,
                ticketRepoMock.Object, 
                unitOfWorkMock.Object);

            // Act
            Result<SubscribeCustomerToEventOutput> output = await useCase.Execute(
                createInput,
                new CancellationToken());

            // Assert
            Assert.Equal(output.Error, EventErrors.NotEnoughSpots);
        }
    }
}
