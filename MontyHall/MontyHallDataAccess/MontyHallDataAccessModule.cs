using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MontyHallDataAccess.Database;
using MontyHallDataAccess.Interfaces;
using System.Reflection;

namespace MontyHallDataAccess
{
    public static class MontyHallDataAccessModule
    {
        public static IServiceCollection AddMontyHallDataAccess(this IServiceCollection services, IConfiguration configuration, params Assembly[] assemblies)
        {
            services.AddScoped<IMontyHallAccess, MontyHallAccess>();
            services.AddDbContext<MontyHallContext>(opt => opt.UseSqlServer(configuration["ConnectionStrings:MontyHallContext"]));

            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumers(assemblies);

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                    cfg.Host(configuration["rabbit:host"], configuration["rabbit:virtualhost"], h =>
                    {
                        h.Username(configuration["rabbit:username"]);
                        h.Password(configuration["rabbit:password"]);
                    });
                });
            });

            return services;
        }

        public static IServiceScope UseDataAccess(this IServiceScope scope)
        {
            var context = scope.ServiceProvider.GetRequiredService<MontyHallContext>();
            context.Database.EnsureCreated();
            return scope;
        }

    }
}
