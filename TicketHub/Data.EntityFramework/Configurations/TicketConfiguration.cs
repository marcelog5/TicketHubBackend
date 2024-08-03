using Domain.Tickets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityFramework.Configurations
{
    internal sealed class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("tickets");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Status)
                .HasConversion(status => status.ToString(),
                               status => Enum.Parse<EnTicketStatus>(status));

            builder.Property(t => t.PaidAt);

            builder.Property(t => t.ReservedAt);

            builder.HasOne(t => t.Customer)
                .WithMany()
                .HasForeignKey(t => t.CustomerId);

            builder.HasOne(t => t.Event)
                .WithMany(e => e.Tickets)
                .HasForeignKey(t => t.EventId);
        }
    }
}
