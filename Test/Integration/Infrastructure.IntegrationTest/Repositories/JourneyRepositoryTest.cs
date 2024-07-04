using Application.Journeys;
using Domain.Journeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Integrationtest.Repositories
{
    public class JourneyRepositoryTest:BaseIntegrationTest
    {
        private readonly IJourneyRepository _journeyRepository;

        public JourneyRepositoryTest(WebApplicationFactory factory):
            base(factory)
        {
            _journeyRepository = _scope.ServiceProvider
                .GetRequiredService<IJourneyRepository>();
        }

        private const string NAME = "Discover Istanbul";
        private const string DESCRIPTION = "Welcome to Istanbul! Visit and discover the history of the most beautiful city in the world";
        private const string COUNTRY = "Turkey";
        private const string CITY = "Istanbul";
        private const decimal PRICE = 650m;

        [Fact]
        public async void GetJourney_ShouldReturnListOfJourneys()
        {
            //Arrange
            await CreateJourney();

            //Act
            var journeys = await _journeyRepository.GetJourneys();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Journey>>(journeys);
            ClearJourneys();
        }

        [Fact]
        public async void Create_ShouldCreteJourney()
        {
            //Arrange
            var journey = new Journey(
                NAME,
                DESCRIPTION,
                COUNTRY,
                CITY,
                PRICE,
                Guid.NewGuid());

            //Act
            await _journeyRepository.Create(journey);

            //Assert
            var createdJourney = await _dbContext.Journeys
                .FirstOrDefaultAsync(j => j.Id == journey.Id);

            Assert.NotNull(createdJourney);
            ClearJourneys();
        }

        [Fact]
        public async void GetById_ShouldReturnExpectedJourney()
        {
            //Arrange
            var journey = await CreateJourney();

            //Act
            var result = await _journeyRepository.GetById(journey.Id);

            //Assert
            Assert.NotNull(result);
            ClearJourneys();
        }

        [Fact]
        public async void Update_ShouldUpdateJourneyPrice()
        {
            //Arrange
            var journey = await CreateJourney();

            //Act
            var newPrice = decimal.Divide(journey.Price, 2);
            journey.Price = newPrice;

            await _journeyRepository.SaveChanges(journey);

            //Assert
            var updatedJourney = await _dbContext.Journeys
                .FirstOrDefaultAsync(j => j.Id == journey.Id);

            Assert.Equal(newPrice, updatedJourney.Price);
            ClearJourneys();
        }

        [Fact]
        public async void Delete_ShouldDeleteJourney()
        {
            //Arrange
            var journey = await CreateJourney();

            //Act
            await _journeyRepository.Delete(journey);

            //Assert
            var deletedJourney = await _dbContext.Journeys
                .FirstOrDefaultAsync(j => j.Id == journey.Id);

            Assert.Null(deletedJourney);
            ClearJourneys();
        }

        private async Task<Journey> CreateJourney()
        {
            var journey = new Journey(
                NAME, 
                DESCRIPTION,
                COUNTRY, 
                CITY, 
                PRICE,
                Guid.NewGuid());

            await _dbContext.Journeys.AddAsync(journey);

            return journey;
        }

        private void ClearJourneys()
        {
            _dbContext.Journeys.RemoveRange(
                _dbContext.Journeys);
        }
    }
}
