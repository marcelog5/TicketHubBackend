using Domain.Customers;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Data.EntityFramework.Repositories
{
    internal sealed class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> AlreadyExist(
            Email email, 
            Cpf cpf, 
            CancellationToken cancellationToken)
        {
            return await DbContext
                .Set<Customer>()
                .AnyAsync(c => c.Email == email || c.Cpf == cpf, cancellationToken);
        }
    }
}
