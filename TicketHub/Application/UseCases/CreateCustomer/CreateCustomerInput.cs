using Domain.Customers;

namespace Application.UseCases.CreateCustomer
{
    public sealed record CreateCustomerInput(
        Name Name,
        Email Email,
        Cpf Cpf);
}
