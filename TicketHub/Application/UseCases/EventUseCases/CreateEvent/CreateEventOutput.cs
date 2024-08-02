using Domain.Shared;

namespace Application.UseCases.EventUseCases.CreateEvent
{
    public sealed record CreateEventOutput(
        Guid Id,
        DateTime Date,
        Name Name,
        Guid PartnerId);
}