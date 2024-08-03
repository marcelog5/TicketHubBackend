using Application.Abstracts;
using Application.UseCases.CustomerUseCases.CreateCustomer;
using Domain.Abstracts;

namespace Domain.Customers.UseCases
{
    public interface ICreateCustomerUseCase : IUseCase<CreateCustomerInput, Result<CreateCustomerOutput>>
    {
    }
}
