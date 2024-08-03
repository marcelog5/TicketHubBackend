using Domain.Partners;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityFramework.Configurations
{
    internal sealed class PartnerConfiguration : IEntityTypeConfiguration<Partner>
    {
        public void Configure(EntityTypeBuilder<Partner> builder)
        {
            builder.ToTable("partners");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Cnpj)
                .HasMaxLength(18)
                .HasConversion(cnpj => cnpj.Value, value => new Cnpj(value));

            builder.Property(p => p.Email)
                .HasMaxLength(400)
                .HasConversion(email => email.Value, value => new Email(value));

            builder.Property(p => p.Name)
                .HasMaxLength(200)
                .HasConversion(name => name.Value, value => new Name(value));

            builder.HasIndex(p => p.Cnpj)
                .IsUnique();

            builder.HasIndex(p => p.Email)
                .IsUnique();
        }
    }
}
