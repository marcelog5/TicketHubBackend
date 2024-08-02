namespace Domain.Events
{
    public interface IEventRepository
    {
        Task Add(Event @event, CancellationToken cancellationToken = default);
    }
}
