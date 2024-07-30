using Application.Abstracts;
using Domain.Customers;

namespace Application.UseCases.CreateCustomer
{
    public sealed class CreateCustomerUseCase : UseCase<CreateCustomerInput, CreateCustomerOutput>
    {
        public override CreateCustomerOutput Execute(CreateCustomerInput input)
        {
            if (input.Cpf is null)
            {
                throw new ArgumentNullException(nameof(input.Cpf));
            }

            if (input.Email is null)
            {
                throw new ArgumentNullException(nameof(input.Email));
            }

            Customer customer = new Customer(
                Guid.NewGuid(),
                input.Name,
                input.Email,
                input.Cpf);

            return new CreateCustomerOutput(
                customer.Id,
                customer.Name,
                customer.Email,
                customer.Cpf);
        }
    }
}
