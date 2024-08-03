using Domain.Tickets;

namespace Data.EntityFramework.Repositories
{
    internal sealed class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        public TicketRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> customerAlreadySubscribed(
            Guid eventId, 
            Guid customerId, 
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
