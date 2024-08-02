using Application.Abstracts;
using Domain.Abstracts;
using Domain.Customers;
using Domain.Events;
using Domain.Tickets;

namespace Application.UseCases.TicketUseCases
{
    public class SubscribeCustomerToEventUseCase : UseCase<SubscribeCustomerToEventInput, Result<SubscribeCustomerToEventOutput>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IEventRepository _eventRepository;
        private readonly ITicketRepository _ticketRepository;

        public SubscribeCustomerToEventUseCase
        (
            ICustomerRepository customerRepository, 
            IEventRepository eventRepository,
            ITicketRepository ticketRepository
        )
        {
            _customerRepository = customerRepository;
            _eventRepository = eventRepository;
            _ticketRepository = ticketRepository;
        }

        public override async Task<Result<SubscribeCustomerToEventOutput>> Execute(
            SubscribeCustomerToEventInput input, 
            CancellationToken cancellationToken = default)
        {
            Customer customer = await _customerRepository.GetById(
                input.CustomerId, cancellationToken);

            if (customer is null)
            {
                return Result.Failure<SubscribeCustomerToEventOutput>(
                    CustomerErrors.NotFound);
            }

            Event @event = await _eventRepository.GetById(
                input.EventId, cancellationToken);

            if (@event is null)
            {
                return Result.Failure<SubscribeCustomerToEventOutput>(
                    EventErrors.NotFound);
            }

            if (@event.TotalSpots < @event.Tickets.Count + 1)
            {
                return Result.Failure<SubscribeCustomerToEventOutput>(
                    EventErrors.NotEnoughSpots);
            }

            var ticket = new Ticket(
                Guid.NewGuid(),
                EnTicketStatus.Pending,
                null,
                DateTime.UtcNow,
                customer.Id,
                @event.Id);

            await _ticketRepository.Add(ticket, cancellationToken);

            return new SubscribeCustomerToEventOutput(
                ticket.Id,
                ticket.Status);
        }
    }
}
