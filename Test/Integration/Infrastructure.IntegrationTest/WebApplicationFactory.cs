using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using Testcontainers.Redis;

namespace Infrastructure.Integrationtest
{
    public class WebApplicationFactory: WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _dbContainer = 
            new PostgreSqlBuilder()
                .WithImage("postgres:latest")
                .WithDatabase("BestJourney")
                .WithUsername("test")
                .WithPassword("secretPassword")
                .Build();

        private readonly RedisContainer _redisContainer =
            new RedisBuilder()
                .WithImage("redis:alpine")
                .WithExposedPort(5432)
                .Build();

        public Task InitializeAsync()
        {
            return Task.Run(async () =>
            {
                await _dbContainer.StartAsync();
                await _redisContainer.StartAsync();
            });
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services
                    .SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<BestJourneyDbContext>));

                if (descriptor is not null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<BestJourneyDbContext>(options =>
                    options.UseNpgsql(_dbContainer.GetConnectionString()));

                var redisDescriptor = services.SingleOrDefault(s => s.ServiceType == typeof(IDistributedCache));

                if(redisDescriptor is not null)
                {
                    services.Remove(redisDescriptor);
                }

                services.AddStackExchangeRedisCache(options =>
                    options.Configuration = _redisContainer.GetConnectionString());
            });
        }

        Task IAsyncLifetime.DisposeAsync()
        {
            return _dbContainer.StopAsync();
        }
    }
}
