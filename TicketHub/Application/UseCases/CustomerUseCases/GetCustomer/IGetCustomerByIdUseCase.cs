using Application.Abstracts;
using Domain.Abstracts;

namespace Application.UseCases.CustomerUseCases.GetCustomer
{
    public interface IGetCustomerByIdUseCase : 
        IUseCase<GetCustomerByIdInput, Result<GetCustomerByIdOutput>>
    {
    }
}
