using Domain.Journeys.Requests;

namespace Domain.UnitTest.DTOs.Journeys
{
    public class CreateJourneyValidatorTest
    {
        [Fact]
        public void Validate_ShouldBeTrue_WhenPayloadIsValidAndActive()
        {
            //Arrange
            var payload = CreateValidPayload(isActive: true);

            //Act
            var result = payload.Validate();

            //Assert
            Assert.True(result);
            Assert.True(payload.IsActive);
        }

        [Fact]
        public void Validate_ShouldBeTrue_WhenPayloadIsValidAndInActive()
        {
            //Arrange
            var payload = CreateValidPayload(isActive: false);

            //Act
            var result = payload.Validate();

            //Assert
            Assert.True(result);
            Assert.False(payload.IsActive);
        }

        [Fact]
        public void Validate_ShouldBeFalse_WhenPayloadNameIsInvalid()
        {
            //Arrange
            var payload = CreateInvalidValidPayload(invalidName: true);

            //Act
            var result = payload.Validate();

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_ShouldBeFalse_WhenPayloadDescriptionIsInvalid()
        {
            //Arrange
            var payload = CreateInvalidValidPayload(invalidDescription: true);

            //Act
            var result = payload.Validate();

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_ShouldBeFalse_WhenPayloadCountryIsInvalid()
        {
            //Arrange
            var payload = CreateInvalidValidPayload(invalidCountry: true);

            //Act
            var result = payload.Validate();

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_ShouldBeFalse_WhenPayloadCityIsInvalid()
        {
            //Arrange
            var payload = CreateInvalidValidPayload(invalidCity: true);

            //Act
            var result = payload.Validate();

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_ShouldBeFalse_WhenPayloadPriceIsInvalid()
        {
            //Arrange
            var payload = CreateInvalidValidPayload(invalidPrice: true);

            //Act
            var result = payload.Validate();

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_ShouldBeFalse_WhenPayloadIsEmpty()
        {
            //Arrange
            var payload = CreateEmptyPayload();

            //Act
            var result = payload.Validate();

            //Assert
            Assert.False(result);
        }

        private static CreateJourneyRequest CreateValidPayload(bool isActive)
        {
            return new CreateJourneyRequest()
            {
                Name = "Discover Istanbul",
                Description = "Welcome to Istanbul! Visit and discover the history of the most beautiful city in the world",
                Country = "Turkey",
                City = "Istanbul",
                Price = 650m,
                IsActive = isActive
            };
        }

        private static CreateJourneyRequest CreateEmptyPayload()
        {
            return new();
        }

        private static CreateJourneyRequest CreateInvalidValidPayload(
            bool invalidName = false,
            bool invalidDescription = false,
            bool invalidCountry = false,
            bool invalidCity = false,
            bool invalidPrice = false)
        {
            return new CreateJourneyRequest()
            {
                Name = invalidName ? "" : "Discover Istanbul",
                Description = invalidDescription ? "" : "Welcome to Istanbul! Visit and discover the history of the most beautiful city in the world",
                Country = invalidCountry ? "" : "Turkey",
                City = invalidCity ? "" : "Istanbul",
                Price = invalidPrice ? 0m: 650m,
                IsActive = true
            };
        }
    }
}
