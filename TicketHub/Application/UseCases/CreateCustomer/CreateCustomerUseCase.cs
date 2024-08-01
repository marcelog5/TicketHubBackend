using Application.Abstracts;
using Domain.Abstracts;
using Domain.Customers;

namespace Application.UseCases.CreateCustomer
{
    public sealed class CreateCustomerUseCase : UseCase<CreateCustomerInput, Result<CreateCustomerOutput>>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerUseCase(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async override Task<Result<CreateCustomerOutput>> Execute(
            CreateCustomerInput input,
            CancellationToken cancellationToken)
        {
            var customerExist = await _customerRepository.CustomerAlreadyExist(
                input.Email,
                input.Cpf,
                cancellationToken);

            if (customerExist)
            {
                return Result.Failure<CreateCustomerOutput>(CustomerErrors.AlreadyExists);
            }

            var customer = new Customer(
                Guid.NewGuid(),
                input.Name,
                input.Email,
                input.Cpf);

            await _customerRepository.AddCustomer(customer);

            return new CreateCustomerOutput(
                customer.Id,
                customer.Name,
                customer.Email,
                customer.Cpf);
        }
    }
}
