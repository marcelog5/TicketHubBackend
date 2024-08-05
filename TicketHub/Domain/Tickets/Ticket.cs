using Domain.Abstracts;
using Domain.Customers;
using Domain.Events;

namespace Domain.Tickets
{
    public sealed class Ticket : Entity
    {
        private Ticket(
            Guid Id,
            EnTicketStatus status,
            DateTime? paidAt,
            DateTime reservedAt,
            Guid customerId,
            Guid eventId,
            int ordering) : base(Id)
        {
            if (eventId == default)
            {
                throw new ArgumentNullException(nameof(eventId));
            }

            if (customerId == default)
            {
                throw new ArgumentNullException(nameof(customerId));
            }

            if (reservedAt == default)
            {
                throw new ArgumentNullException(nameof(reservedAt));
            }

            if (ordering <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ordering));
            }

            Status = status;
            PaidAt = paidAt;
            ReservedAt = reservedAt;
            CustomerId = customerId;
            EventId = eventId;
            Ordering = ordering;
        }

        private Ticket()
        {
        }

        public static Ticket Create(
            EnTicketStatus status,
            DateTime? paidAt,
            DateTime reservedAt,
            int ordering,
            Guid customerId,
            Guid eventId)
        {
            return new Ticket(
                Guid.NewGuid(),
                status: status,
                paidAt: paidAt,
                reservedAt: reservedAt,
                customerId: customerId,
                eventId: eventId,
                ordering: ordering);
        }

        public EnTicketStatus Status { get; private set; }
        public DateTime? PaidAt { get; private set; }
        public DateTime ReservedAt { get; private set; }
        public Guid CustomerId { get; private set; }
        public Customer Customer { get; private set; }
        public Guid EventId { get; private set; }
        public Event Event { get; private set; }
        public int Ordering { get; private set; }
    }
}
