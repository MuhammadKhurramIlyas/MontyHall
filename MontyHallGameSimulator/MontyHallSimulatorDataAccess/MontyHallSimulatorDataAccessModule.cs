using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MontyHallSimulatorDataAccess.Database;
using MontyHallSimulatorDataAccess.Interfaces;

namespace MontyHallSimulatorDataAccess
{
    public static class MontyHallSimulatorDataAccessModule
    {
        public static IServiceCollection AddMontyHallSimulatorDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MontyHallSimulatorDb");
            services.AddDbContext<MontyHallSimulatorDataContext>(opt => opt.UseSqlServer(connectionString));
            services.AddScoped<IMontyHallSimulatorDataAccess, MontyHallSimulatorDataAccess>();
            return services;
        }
    }
}
