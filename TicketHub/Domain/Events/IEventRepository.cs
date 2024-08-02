namespace Domain.Events
{
    public interface IEventRepository
    {
        Task<Event?> GetById(Guid id, CancellationToken cancellationToken = default);
        Task Add(Event @event, CancellationToken cancellationToken = default);
    }
}
