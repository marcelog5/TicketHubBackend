using Domain.Events;

namespace Data.EntityFramework.Repositories
{
    internal sealed class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
