using Domain.Customers;
using Domain.Shared;

namespace Application.UseCases.CustomerUseCases.CreateCustomer
{
    public sealed record CreateCustomerInput(
        Name Name,
        Email Email,
        Cpf Cpf);
}
