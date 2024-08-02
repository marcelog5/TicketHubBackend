using Domain.Shared;

namespace Application.UseCases.EventUseCases.CreateEvent
{
    public sealed record CreateEventInput(
        DateTime DateTime,
        Name Name,
        int TotalSpots,
        Guid PartnerId);
}