using Domain.Abstracts;
using Domain.Customers;
using Domain.Customers.UseCases;

namespace Application.UseCases.CustomerUseCases.CreateCustomer
{
    public sealed class CreateCustomerUseCase : ICreateCustomerUseCase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerUseCase(
            ICustomerRepository customerRepository, 
            IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<CreateCustomerOutput>> Execute(
            CreateCustomerInput input,
            CancellationToken cancellationToken)
        {
            var customerExist = await _customerRepository.AlreadyExist(
                input.Email,
                input.Cpf,
                cancellationToken);

            if (customerExist)
            {
                return Result.Failure<CreateCustomerOutput>(CustomerErrors.AlreadyExist);
            }

            var customer = Customer.Create(
                input.Name,
                input.Email,
                input.Cpf);

            await _customerRepository.Add(customer, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CreateCustomerOutput(
                customer.Id,
                customer.Name,
                customer.Email,
                customer.Cpf);
        }
    }
}
