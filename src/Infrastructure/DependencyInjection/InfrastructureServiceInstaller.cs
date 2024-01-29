using Application.Interfaces.Hasher;
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
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<BestJourneyDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IHashService, HashService>();

            return services;
        }
    }
}
