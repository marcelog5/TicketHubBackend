using Application.Abstracts;
using Domain.Customers;

namespace Application.UseCases.CreateCustomer
{
    public sealed class CreateCustomerUseCase : UseCase<CreateCustomerInput, CreateCustomerOutput>
    {
        private readonly ICustomerRepository _customerRepository;
        public CreateCustomerUseCase
        (
            ICustomerRepository customerRepository
        )
        {
            _customerRepository = customerRepository;
        }

        public async override Task<CreateCustomerOutput> Execute(
            CreateCustomerInput input, 
            CancellationToken cancellationToken)
        {
            bool customerExist = (await _customerRepository.CustomerAlreadyExist(
                input.Email, 
                input.Cpf, 
                cancellationToken)) is not null;

            if (customerExist)
            {
                throw new Exception("Customer already exist.");
            }

            Customer customer = new Customer(
                Guid.NewGuid(),
                input.Name,
                input.Email,
                input.Cpf);

            _customerRepository.AddCustomer(customer);

            return new CreateCustomerOutput(
                customer.Id,
                customer.Name,
                customer.Email,
                customer.Cpf);
        }
    }
}
