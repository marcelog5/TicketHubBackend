namespace Domain.Tickets
{
    public interface ITicketRepository
    {
        Task<bool> customerAlreadySubscribed(Guid eventId, Guid customerId, CancellationToken cancellationToken = default);
        Task Add(Ticket ticket, CancellationToken cancellationToken = default);
    }
}
