using Domain.Customers;

namespace Application.UseCases.CreateCustomer
{
    public sealed record CreateCustomerOutput(
        Guid Id,
        Name Name,
        Email Email,
        Cpf Cpf);
}
