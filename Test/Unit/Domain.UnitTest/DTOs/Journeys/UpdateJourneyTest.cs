using Domain.Journeys.Requests;

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

        private static UpdateJourneyRequest CreateValidPayload(bool isActive)
        {
            return new UpdateJourneyRequest()
            {
                Price = 650m,
            };
        }

        private static UpdateJourneyRequest CreateEmptyPayload()
        {
            return new UpdateJourneyRequest();
        }
    }
}
