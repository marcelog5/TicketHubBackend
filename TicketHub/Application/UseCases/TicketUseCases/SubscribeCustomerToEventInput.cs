namespace Application.UseCases.TicketUseCases
{
    public sealed record SubscribeCustomerToEventInput(
        Guid EventId,
        Guid CustomerId);
}