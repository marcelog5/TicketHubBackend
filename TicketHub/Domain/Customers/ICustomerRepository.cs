using Domain.Shared;

namespace Domain.Customers
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetById(Guid id, CancellationToken cancellationToken);
        Task<bool> AlreadyExist(Email email, Cpf cpf, CancellationToken cancellationToken);
        Task Add(Customer customer, CancellationToken cancellationToken);
    }
}
