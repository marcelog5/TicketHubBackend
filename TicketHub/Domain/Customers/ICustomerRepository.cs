namespace Domain.Customers
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerById(Guid id, CancellationToken cancellationToken);
        Task<Customer> CustomerAlreadyExist(Email email, Cpf cpf, CancellationToken cancellationToken);
        void AddCustomer(Customer customer);
    }
}
