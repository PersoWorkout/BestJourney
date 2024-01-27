using Application.Interfaces.Users;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IntegrationTest
{
    public abstract class BasicIntegrationTest: IClassFixture<WebApplicationFactory>
    {
        protected readonly IUserRepository _userRepository;
        protected readonly BestJourneyDataContext _dbContext;

        public BasicIntegrationTest(WebApplicationFactory factory)
        {
            var scope = factory.Services.CreateScope();
            _userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
            _dbContext = scope.ServiceProvider.GetRequiredService<BestJourneyDataContext>();
        }
    }
}
