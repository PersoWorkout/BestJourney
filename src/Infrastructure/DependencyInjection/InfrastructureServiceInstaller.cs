using Application.Auth;
using Application.Journeys;
using Application.Orders;
using Application.Users;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection;

public static class InfrastructureServiceInstaller
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, 
        IConfiguration configuration) 
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        var dbConnectionString = configuration
            .GetConnectionString("SqliteConnectionString");
        services.AddDbContext<BestJourneyDbContext>(options =>
            options.UseSqlite(dbConnectionString));

        var redisConnectionString = configuration
            .GetConnectionString("RedisConnectionString");
        services.AddDistributedMemoryCache();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IJourneyRepository, JourneyRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        services.AddScoped<IHashService, HashService>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
