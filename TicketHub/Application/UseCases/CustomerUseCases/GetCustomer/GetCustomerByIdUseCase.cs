using Domain.Abstracts;
using Domain.Customers;

namespace Application.UseCases.CustomerUseCases.GetCustomer
{
    public sealed class GetCustomerByIdUseCase : IGetCustomerByIdUseCase
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerByIdUseCase
        (
            ICustomerRepository customerRepository
        )
        {
            _customerRepository = customerRepository;
        }

        public async Task<Result<GetCustomerByIdOutput>> Execute(
            GetCustomerByIdInput input,
            CancellationToken cancellationToken = default)
        {
            Customer customer = await _customerRepository.GetById(
                input.Id,
                cancellationToken);

            if (customer == null)
            {
                return Result.Failure<GetCustomerByIdOutput>(CustomerErrors.NotFound);
            }

            return new GetCustomerByIdOutput(
                customer.Name,
                customer.Email,
                customer.Cpf
            );
        }
    }
}
