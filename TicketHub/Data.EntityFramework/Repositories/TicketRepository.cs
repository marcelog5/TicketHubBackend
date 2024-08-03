using Domain.Tickets;
using Microsoft.EntityFrameworkCore;

namespace Data.EntityFramework.Repositories
{
    internal sealed class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        public TicketRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> customerAlreadySubscribed(
            Guid eventId, 
            Guid customerId, 
            CancellationToken cancellationToken = default)
        {
            return await DbContext.
                Set<Ticket>()
                .AnyAsync(t => t.EventId == eventId && t.CustomerId == customerId, cancellationToken);
        }
    }
}
