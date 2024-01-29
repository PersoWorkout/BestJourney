using Domain.DTOs.Validators.Journeys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private static CreateJourneyValidator CreateValidPayload(bool isActive)
        {
            return new CreateJourneyValidator()
            {
                Name = "Discover Istanbul",
                Description = "Welcome to Istanbul! Visit and discover the history of the most beautiful city in the world",
                Country = "Turkey",
                City = "Istanbul",
                Price = 650m,
                IsActive = isActive
            };
        }

        private static CreateJourneyValidator CreateEmptyPayload()
        {
            return new();
        }

        private static CreateJourneyValidator CreateInvalidValidPayload(
            bool invalidName = false,
            bool invalidDescription = false,
            bool invalidCountry = false,
            bool invalidCity = false,
            bool invalidPrice = false)
        {
            return new CreateJourneyValidator()
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
