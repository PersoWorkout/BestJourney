using Domain.Journeys.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UnitTest.DTOs.Journeys
{
    public class UpdateJourneyTest
    {
        [Fact]
        public void Validate_ShouleBeTrue_WhenPayloadIsValid()
        {
            //Arrange
            var payload = CreateValidPayload(true);

            //Act
            var result = payload.Validate();

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_ShouleBeFalse_WhenPayloadIsEmpty()
        {
            //Arrange
            var payload = CreateEmptyPayload();

            //Act
            var result = payload.Validate();

            //Assert
            Assert.False(result);
        }

        private static UpdateJourneyValidator CreateValidPayload(bool isActive)
        {
            return new UpdateJourneyValidator()
            {
                Price = 650m,
            };
        }

        private static UpdateJourneyValidator CreateEmptyPayload()
        {
            return new UpdateJourneyValidator();
        }
    }
}
