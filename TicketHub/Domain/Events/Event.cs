using Domain.Abstracts;
using Domain.Shared;

namespace Domain.Events
{
    public sealed class Event : Entity
    {
        public Event(
            Guid id, 
            DateTime date, 
            Name name, 
            int totalSpots, 
            Guid partnerId) : base(id)
        {
            Date = date;
            Name = name;
            TotalSpots = totalSpots;
            PartnerId = partnerId;
        }

        public DateTime Date { get; }
        public Name Name { get; }
        public int TotalSpots { get; }
        public Guid PartnerId { get; }
    }
}
