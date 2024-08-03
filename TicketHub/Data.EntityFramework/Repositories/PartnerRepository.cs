using Domain.Partners;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Data.EntityFramework.Repositories
{
    internal sealed class PartnerRepository : Repository<Partner>, IPartnerRepository
    {
        public PartnerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> AlreadyExist(
            Email email, 
            Cnpj cnpj, 
            CancellationToken cancellationToken = default)
        {
            return await DbContext
                .Set<Partner>()
                .AnyAsync(p => p.Email == email || p.Cnpj == cnpj, cancellationToken);
        }
    }
}
