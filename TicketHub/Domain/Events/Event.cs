using Domain.Abstracts;
using Domain.Partners;
using Domain.Shared;
using Domain.Tickets;

namespace Domain.Events
{
    public sealed class Event : Entity
    {
        private Event(
            Guid Id, 
            DateTime date, 
            Name name, 
            int totalSpots, 
            Guid partnerId) : base(Id)
        {
            if (date < DateTime.Now)
            {
                throw new Exception("Event date must be in the future");
            }

            if (totalSpots <= 0)
            {
                throw new Exception("Event must have at least one spot");
            }

            if (partnerId == Guid.Empty)
            {
                throw new Exception("Event must have a partner");
            }

            Date = date;
            Name = name;
            TotalSpots = totalSpots;
            PartnerId = partnerId;
        }

        private Event()
        {
        }

        public static Event Create(
            DateTime date, 
            Name name, 
            int totalSpots, 
            Guid partnerId)
        {
            return new Event(Guid.NewGuid(), date, name, totalSpots, partnerId);
        }

        public Result<Ticket> ReserveTicket(Guid customerId)
        {
            bool ticketAlreadyExist = Tickets
                .Where(t => t.CustomerId == customerId)
                .FirstOrDefault() is not null;

            if (ticketAlreadyExist)
            {
                return Result.Failure<Ticket>(TicketErrors.CustomerAlreadySubscribed);
            }

            if (TotalSpots < Tickets.Count + 1)
            {
                return Result.Failure<Ticket>(EventErrors.NotEnoughSpots);
            }

            var ticket = Ticket.Create(
                EnTicketStatus.Pending,
                null,
                DateTime.UtcNow,
                Tickets.Count + 1,
                customerId,
                Id);

            Tickets.Add(ticket);

            return ticket;
        } 

        public DateTime Date { get; private set; }
        public Name Name { get; private set; }
        public int TotalSpots { get; private set; }
        public Guid PartnerId { get; private set; }
        public Partner Partner { get; private set; }
        public HashSet<Ticket> Tickets { get; private set; } = new HashSet<Ticket>();
    }
}
