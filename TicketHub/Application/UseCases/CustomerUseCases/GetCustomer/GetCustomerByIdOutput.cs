using Domain.Customers;
using Domain.Shared;

namespace Application.UseCases.CustomerUseCases.GetCustomer
{
    public sealed record GetCustomerByIdOutput(
        Name Name,
        Email Email,
        Cpf Cpf);
}
