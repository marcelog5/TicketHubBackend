using Domain.Customers;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityFramework.Configurations
{
    internal sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("customers");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Cpf)
                .HasMaxLength(14)
                .HasConversion(cpf => cpf.Value, value => new Cpf(value));

            builder.Property(c => c.Email)
                .HasMaxLength(400)
                .HasConversion(email => email.Value, value => new Email(value));

            builder.Property(c => c.Name)
                .HasMaxLength(200)
                .HasConversion(name => name.Value, value => new Name(value));

            builder.HasIndex(c => c.Cpf)
                .IsUnique();

            builder.HasIndex(c => c.Email)
                .IsUnique();
        }
    }
}
