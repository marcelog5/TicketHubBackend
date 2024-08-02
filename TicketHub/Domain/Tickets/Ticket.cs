using Domain.Abstracts;
using Domain.Customers;
using Domain.Events;

namespace Domain.Tickets
{
    public sealed class Ticket : Entity
    {
        public Ticket(
            Guid id,
            EnTicketStatus status, 
            DateTime? paidAt, 
            DateTime reservedAt, 
            Guid customerId, 
            Guid eventId) : base(id)
        {
            Status = status;
            PaidAt = paidAt;
            ReservedAt = reservedAt;
            CustomerId = customerId;
            EventId = eventId;
        }

        public EnTicketStatus Status { get; private set; }
        public DateTime? PaidAt { get; private set; }
        public DateTime ReservedAt { get; private set; }
        public Guid CustomerId { get; private set; }
        public Customer Customer { get; private set; }
        public Guid EventId { get; private set; }
        public Event Event { get; private set; }
    }
}
