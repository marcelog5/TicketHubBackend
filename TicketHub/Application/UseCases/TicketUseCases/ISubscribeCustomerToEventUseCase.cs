using Application.Abstracts;
using Domain.Abstracts;

namespace Application.UseCases.TicketUseCases
{
    public interface ISubscribeCustomerToEventUseCase : 
        IUseCase<SubscribeCustomerToEventInput, Result<SubscribeCustomerToEventOutput>>
    {
    }
}
