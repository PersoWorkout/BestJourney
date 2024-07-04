using Application.Journeys;
using AutoMapper;
using Domain.Journeys;
using Domain.Journeys.Requests;

namespace Application.UnitTest.Mappers
{
    public class JourneyProfileTest
    {
        private readonly IMapper _mapper;

        public JourneyProfileTest() 
        {
            MapperConfiguration configuration = new(config =>
            {
                config.CreateMap<string, Guid>().ConvertUsing(value => new Guid(value));
                config.CreateMap<Guid, string>().ConvertUsing(value => value.ToString());
                config.AddProfile(new JourneyProfile());
            });

            _mapper = new Mapper(configuration);
        }

        private const string NAME = "Discover Istanbul";
        private const string DESCRIPTION = "Welcome to Istanbul! Visit and discover the history of the most beautiful city in the world";
        private const string COUNTRY = "Turkey";
        private const string CITY = "Istanbul";
        private const decimal PRICE = 650m;

        [Fact]
        public void Map_ShouldReturnJourneyResponse_WhenParameterIsJourney()
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
            var result = _mapper.Map<JourneyResponse>(journey);

            //Assert
            Assert.IsType<JourneyResponse>(result);
        }

        [Fact]
        public void Map_ShouldReturnJourney_WhenParameterIsJourneyValidator()
        {
            //Arrange
            var payload = new CreateJourneyRequest
            {
                Name = NAME,
                Description = DESCRIPTION,
                Country = COUNTRY,
                City = CITY,
                Price = PRICE,
            };

            //Act
            var result = _mapper.Map<Journey>(payload);

            //Assert
            Assert.IsType<Journey>(result);
        }
    }
}
