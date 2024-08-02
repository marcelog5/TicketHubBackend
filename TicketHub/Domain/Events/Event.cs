using Domain.Abstracts;
using Domain.Partners;
using Domain.Shared;
using Domain.Tickets;

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

        public DateTime Date { get; private set; }
        public Name Name { get; private set; }
        public int TotalSpots { get; private set; }
        public Guid PartnerId { get; private set; }
        public Partner Partner { get; private set; }
        public IList<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
