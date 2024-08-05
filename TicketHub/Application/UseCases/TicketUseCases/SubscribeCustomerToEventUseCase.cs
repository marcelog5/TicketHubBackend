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

            var ticket = @event.ReserveTicket(input.CustomerId);

            if (ticket.IsFailure)
            {
                return Result.Failure<SubscribeCustomerToEventOutput>(
                                       ticket.Error);
            }

            await _ticketRepository.Add(ticket.Value, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new SubscribeCustomerToEventOutput(
                ticket.Value.Id,
                ticket.Value.Status);
        }
    }
}
