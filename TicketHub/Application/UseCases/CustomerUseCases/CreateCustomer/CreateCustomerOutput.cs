using Domain.Customers;
using Domain.Shared;

namespace Application.UseCases.CustomerUseCases.CreateCustomer
{
    public sealed record CreateCustomerOutput(
        Guid Id,
        Name Name,
        Email Email,
        Cpf Cpf);
}
