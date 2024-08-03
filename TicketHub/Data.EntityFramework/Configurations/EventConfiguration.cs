using Domain.Events;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityFramework.Configurations
{
    internal sealed class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("events");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Date);

            builder.Property(e => e.Name)
                .HasMaxLength(200)
                .HasConversion(name => name.Value, value => new Name(value));

            builder.Property(e => e.TotalSpots);

            builder.HasOne(e => e.Partner)
                .WithMany()
                .HasForeignKey(e => e.PartnerId);

            builder.HasMany(e => e.Tickets)
                .WithOne()
                .HasForeignKey(t => t.EventId);
        }
    }
}
