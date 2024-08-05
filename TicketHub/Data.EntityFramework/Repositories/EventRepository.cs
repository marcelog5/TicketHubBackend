using Domain.Events;
using Microsoft.EntityFrameworkCore;

namespace Data.EntityFramework.Repositories
{
    internal sealed class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async override Task<Event?> GetById(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await DbContext
                .Set<Event>()
                .Include(e => e.Tickets)
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }
    }
}
