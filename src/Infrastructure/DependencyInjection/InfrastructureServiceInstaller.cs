using Application.Interfaces;
using Application.Interfaces.Journeys;
using Application.Interfaces.Users;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceInstaller
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services, 
            IConfiguration configuration) 
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<BestJourneyDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJourneyRepository, JourneyRepository>();
            services.AddScoped<IHashService, HashService>();

            return services;
        }
    }
}
