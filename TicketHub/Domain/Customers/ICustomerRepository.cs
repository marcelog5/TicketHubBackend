namespace Domain.Customers
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetCustomerById(Guid id, CancellationToken cancellationToken);
        Task<bool> CustomerAlreadyExist(Email email, Cpf cpf, CancellationToken cancellationToken);
        Task AddCustomer(Customer customer);
    }
}
