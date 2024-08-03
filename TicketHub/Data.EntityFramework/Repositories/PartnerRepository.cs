using Domain.Partners;
using Domain.Shared;

namespace Data.EntityFramework.Repositories
{
    internal sealed class PartnerRepository : Repository<Partner>, IPartnerRepository
    {
        public PartnerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> AlreadyExist(Email email, Cnpj cnpj, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
