﻿using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

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

        public Task InitializeAsync()
        {
            return _dbContainer.StartAsync(); ;
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
            });
        }

        Task IAsyncLifetime.DisposeAsync()
        {
            return _dbContainer.StopAsync();
        }
    }
}
