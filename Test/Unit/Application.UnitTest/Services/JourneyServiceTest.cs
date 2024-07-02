using Application.Interfaces.Journeys;
using Application.Services;
using Application.UnitTest.Fakers.Journeys;
using Domain.Journeys;
using Domain.Journeys.Validators;

namespace Application.UnitTest.Services
{
    public class JourneyServiceTest
    {
        private readonly IJourneyService _journeyService;
        private readonly FakeJourneyRepository _fakeJourneyRepository;

        private const string NAME = "Discover Istanbul";
        private const string DESCRIPTION = "Welcome to Istanbul! Visit and discover the history of the most beautiful city in the world";
        private const string COUNTRY = "Turkey";
        private const string CITY = "Istanbul";
        private const decimal PRICE = 650m;

        public JourneyServiceTest()
        {
            _fakeJourneyRepository = new FakeJourneyRepository();
            var mapper = FakeJourneyMapper.Create();

            _journeyService = new JourneyService(_fakeJourneyRepository, mapper);
        }

        [Fact]
        public async void GetJourneys_SouldBeSuccess()
        {
            //Arrange
            //Act
            var result = await _journeyService.GetJourneys();

            //Assert
            Assert.True(result.IsSucess);

        }

        [Fact]
        public async void Create_SouldBeSuccess_WhenPayloadIsValid()
        {
            //Arrange
            var payload = CreatePayloadToAdd();

            //Act
            var result = await _journeyService.Create(payload);

            //Assert
            Assert.True(result.IsSucess);
            Assert.Equal(result.Data?.Name, payload.Name);
        }

        [Fact]
        public async void Create_ShouldBeFailure_WhenPayloadIsInvalid()
        {
            //Arrange
            var payload = new CreateJourneyValidator();

            //Act
            var result = await _journeyService.Create(payload);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Equal(JourneyError.InvalidPayload, result.Error);
        }

        [Fact]
        public async void GetById_ShouldBeSuccess_WhenJourneyExist()
        {
            //Arrange
            var journey = await CreateJourney();

            //Act
            var result = await _journeyService.GetById(journey.Id.ToString());

            //Assert
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void GetById_ShoulBeFailure_WhenJounreyNotExist()
        {
            //Arrange 
            var id = new Guid();

            //Act
            var result = await _journeyService.GetById(id.ToString());

            //Assert
            Assert.True(result.IsFailure);
            Assert.Equal(JourneyError.NotFound(id.ToString()), result.Error);
        }

        [Fact]
        public async void Update_ShoulBeSuccess_WhenJounreyExistAndPayloadIsValid()
        {
            //Arrange 
            var journey = await CreateJourney();
            var newPrice = decimal.Divide(journey.Price, 2);
            var payload = CreatePayloadToUpdate(price: newPrice);

            //Act
            var result = await _journeyService.Update(journey.Id.ToString(), payload);

            //Assert
            Assert.True(result.IsSucess);
            Assert.Equal(newPrice, journey.Price);
        }

        [Fact]
        public async void Update_ShoulBeFailure_WhenPayloadIsInvalid()
        {
            //Arrange 
            var payload = CreatePayloadToUpdate();

            //Act
            var result = await _journeyService.Update(new Guid().ToString(), payload);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Equal(JourneyError.InvalidPayload, result.Error);
        }

        [Fact]
        public async void Update_ShoulBeFailure_WhenInvalidJourneyId()
        {
            //Arrange 
            var id = "InvalidId";
            var payload = CreatePayloadToUpdate(name: "Travel in Istanbul");

            //Act
            var result = await _journeyService.Update(id, payload);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Equal(JourneyError.NotFound(id), result.Error);
        }

        [Fact]
        public async void Update_ShoulBeFailure_WhenJourneyNotExist()
        {
            //Arrange 
            var id = new Guid();
            var payload = CreatePayloadToUpdate(name: "Travel in Istanbul");

            //Act
            var result = await _journeyService.Update(id.ToString(), payload);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Equal(JourneyError.NotFound(id.ToString()), result.Error);
        }

        [Fact]
        public async void Delete_ShoulBeSuccess_WhenJourneyExist()
        {
            //Arrange 
            var journey = await CreateJourney();

            //Act
            var result = await _journeyService.Delete(journey.Id.ToString());

            //Assert
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void Delete_ShoulBeFailure_WhenInvalidId()
        {
            //Arrange 
            var journeyId = "InvalidId";

            //Act
            var result = await _journeyService.Delete(journeyId.ToString());

            //Assert
            Assert.True(result.IsFailure);
            Assert.Equal(JourneyError.NotFound(journeyId), result.Error);
        }

        [Fact]
        public async void Delete_ShoulBeFailure_WhenJourneyNotExist()
        {
            //Arrange 
            var journeyId = new Guid();

            //Act
            var result = await _journeyService.Delete(journeyId.ToString());

            //Assert
            Assert.True(result.IsFailure);
            Assert.Equal(JourneyError.NotFound(journeyId.ToString()), result.Error);
        }

        private async Task<Journey> CreateJourney()
        {
            var journey = new Journey(
                NAME, 
                DESCRIPTION, 
                CITY, 
                COUNTRY, 
                PRICE);

            await _fakeJourneyRepository.Create(journey);

            return journey;
        }

        private static CreateJourneyValidator CreatePayloadToAdd()
        {
            return new CreateJourneyValidator()
            {
                Name = "Discover Istanbul",
                Description = "Welcome to Istanbul! Visit and discover the history of the most beautiful city in the world",
                Country = "Turkey",
                City = "Istanbul",
                Price = 650m,
                IsActive = true
            };
        }

        private static UpdateJourneyValidator CreatePayloadToUpdate(
            string name = "",
            string description = "",
            string country = "",
            string city = "",
            decimal price = 0m)
        {
            return new UpdateJourneyValidator()
            {
                Name = name,
                Description = description,
                Country = country,
                City = city,
                Price = price,
            };
        }
    }
}