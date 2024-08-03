using Application.Abstracts;
using Domain.Abstracts;

namespace Application.UseCases.EventUseCases.CreateEvent
{
    public interface ICreateEventUseCase : IUseCase<CreateEventInput, Result<CreateEventOutput>>
    {
    }
}
