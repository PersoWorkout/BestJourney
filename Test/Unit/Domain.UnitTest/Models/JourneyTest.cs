using Domain.Models;
using Domain.UnitTest.Utils;

namespace Domain.UnitTest.Models
{
    public class JourneyTest
    {
        private const string NAME = "Test Journey";
        private const string DESCRIPTION = "lorem ipsum dolor sit amet";
        private const string COUNTRY = "Fance";
        private const string CITY = "Paris";
        private const decimal PRICE = 100;


        [Fact]
        public void Create_ShouldCreateActiveJourney_WhenIsActiveIsDefault()
        {
            //Arrange
            //Assert
            var journey = InstanceNewActiveJourney();

            //Assert
            Assert.True(ValidateJourneyCreation(journey, true));
        }

        [Fact]
        public void Create_ShouldCreateNonActiveJourney_WhenIsActiveFalse()
        {
            //Arrange
            //Assert
            var journey = InstanceNewNonActiveJourney();

            //Assert
            Assert.True(ValidateJourneyCreation(journey, false));
        }

        [Fact]
        public void Update_ShouldUpdateJourney()
        {
            //Arrange
            var journey = InstanceNewActiveJourney();

            string name = "New Test Journey";
            string description = LoremIpsum.Generate();
            string country = "Turkey";
            string city = "Istanbul";
            decimal price = 145.5m;
            bool isActive = false;

            //Act
            journey.Update(
                name, 
                description, 
                country, 
                city, 
                price, 
                isActive);

            //Assert
            Assert.True(ValidateJourneyUpdate(
                journey, 
                name, 
                description,
                country, 
                city, 
                price, 
                isActive));
        }

        private static Journey InstanceNewActiveJourney()
        {
            return new(
                NAME,
                DESCRIPTION,
                COUNTRY,
                CITY,
                PRICE) ;
        }

        private static Journey InstanceNewNonActiveJourney()
        {
            return new(
                NAME,
                DESCRIPTION,
                COUNTRY,
                CITY,
                PRICE,
                false);
        }

        private static bool ValidateJourneyCreation(Journey journey, bool isActive)
        {
            return journey.Name == NAME &&
                journey.Description == DESCRIPTION &&
                journey.Country == COUNTRY &&
                journey.Price == PRICE &&
                journey.IsActive == isActive &&
                journey.CreatedAt < DateTime.Now;
        }

        private static bool ValidateJourneyUpdate(
            Journey journey, 
            string name, 
            string description, 
            string country, 
            string city, 
            decimal price, 
            bool isActive)
        {
            return journey.Name == name &&
                journey.Description == description &&
                journey.Country == country &&
                journey.City == city &&
                journey.Price == price &&
                journey.IsActive == isActive&&
                journey.UpdatedAt.HasValue;
        }
    }
}
