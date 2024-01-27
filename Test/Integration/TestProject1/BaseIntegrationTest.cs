using Application.Interfaces.Users;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Integrationtest
{
    public abstract class BaseIntegrationTest: IClassFixture<WebApplicationFactory>
    {
        protected readonly IUserRepository _userRepository;
        protected readonly BestJourneyDbContext _dbContext;

        public BaseIntegrationTest(WebApplicationFactory factory)
        {
            var scope = factory.Services.CreateScope();

            _userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
            _dbContext = scope.ServiceProvider.GetRequiredService<BestJourneyDbContext>();
        }
    }
}
