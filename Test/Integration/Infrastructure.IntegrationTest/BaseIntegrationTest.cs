using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Integrationtest
{
    public abstract class BaseIntegrationTest: IClassFixture<WebApplicationFactory>
    {
        protected readonly IServiceScope _scope;
        protected readonly BestJourneyDbContext _dbContext;

        public BaseIntegrationTest(WebApplicationFactory factory)
        {
            _scope = factory.Services.CreateScope();
            _dbContext = _scope.ServiceProvider.GetRequiredService<BestJourneyDbContext>();
        }
    }
}
