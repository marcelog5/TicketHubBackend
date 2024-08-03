using Domain.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace Data.EntityFramework.Repositories
{
    public abstract class Repository<T>
        where T : Entity
    {
        protected readonly ApplicationDbContext DbContext;

        protected Repository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<T?> GetById(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await DbContext
                .Set<T>()
                .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
        }

        public async Task Add(
            T entity, 
            CancellationToken cancellationToken = default)
        {
            await DbContext
                .AddAsync(entity, cancellationToken);
        }
    }
}
