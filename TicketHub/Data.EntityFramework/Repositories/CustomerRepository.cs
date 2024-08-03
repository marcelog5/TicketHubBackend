using Domain.Customers;
using Domain.Shared;

namespace Data.EntityFramework.Repositories
{
    internal sealed class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> AlreadyExist(Email email, Cpf cpf, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
