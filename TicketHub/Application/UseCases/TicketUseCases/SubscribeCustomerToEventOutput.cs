using Domain.Tickets;

namespace Application.UseCases.TicketUseCases
{
    public sealed record SubscribeCustomerToEventOutput(
        Guid TicketId,
        EnTicketStatus Status);
}