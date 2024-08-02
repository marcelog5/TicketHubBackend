namespace Domain.Tickets
{
    public interface ITicketRepository
    {
        Task Add(Ticket ticket, CancellationToken cancellationToken = default);
    }
}
