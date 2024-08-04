using Domain.Abstracts;
using Domain.Customers;
using Domain.Events;
using Domain.Tickets;

namespace Application.UseCases.TicketUseCases
{
    public class SubscribeCustomerToEventUseCase : ISubscribeCustomerToEventUseCase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IEventRepository _eventRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SubscribeCustomerToEventUseCase
        (
            ICustomerRepository customerRepository, 
            IEventRepository eventRepository,
            ITicketRepository ticketRepository,
            IUnitOfWork unitOfWork
        )
        {
            _customerRepository = customerRepository;
            _eventRepository = eventRepository;
            _ticketRepository = ticketRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<SubscribeCustomerToEventOutput>> Execute(
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

            if (@event is null)
            {
                return Result.Failure<SubscribeCustomerToEventOutput>(
                    EventErrors.NotFound);
            }

            bool customerAlreadySubscribed = await _ticketRepository.customerAlreadySubscribed(
                @event.Id,
                customer.Id,
                cancellationToken);

            if (customerAlreadySubscribed)
            {
               return Result.Failure<SubscribeCustomerToEventOutput>(
                    TicketErrors.CustomerAlreadySubscribed);
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

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new SubscribeCustomerToEventOutput(
                ticket.Id,
                ticket.Status);
        }
    }
}
