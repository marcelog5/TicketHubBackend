using Domain.Abstracts;

namespace Domain.Tickets
{
    public static class TicketErrors
    {
        public static Error CustomerAlreadySubscribed = new(
         "Ticket.AlreadyHaveActiveTicket",
         "The customer already has an active ticket");
    }
}
