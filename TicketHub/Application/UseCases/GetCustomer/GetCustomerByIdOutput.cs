using Domain.Customers;

namespace Application.UseCases.GetCustomer
{
    public sealed record GetCustomerByIdOutput(
        Name Name,
        Email Email,
        Cpf Cpf);
}
