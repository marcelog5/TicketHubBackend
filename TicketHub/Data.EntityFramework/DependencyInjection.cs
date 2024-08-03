using Data.EntityFramework.Repositories;
using Domain.Abstracts;
using Domain.Customers;
using Domain.Events;
using Domain.Partners;
using Domain.Tickets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data.EntityFramework
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataEfCore(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString =
                            configuration.GetConnectionString("Database") ??
                            throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString).UseSnakeCaseNamingConvention();
            });

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IPartnerRepository, PartnerRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

            return services;
        }
    }
}
