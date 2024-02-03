using Application.Interfaces;
using Application.Interfaces.Auth;
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

            var dbConnectionString = configuration
                .GetConnectionString("DefaultConnectionString");
            services.AddDbContext<BestJourneyDbContext>(options =>
                options.UseNpgsql(dbConnectionString));

            var redisConnectionString = configuration
                .GetConnectionString("RedisConnectionString");
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnectionString ;
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJourneyRepository, JourneyRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();

            services.AddScoped<IHashService, HashService>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
