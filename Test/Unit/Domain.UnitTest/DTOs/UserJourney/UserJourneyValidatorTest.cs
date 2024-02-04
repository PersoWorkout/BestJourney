using Domain.DTOs.Validators.UserJourney;

namespace Domain.UnitTest.DTOs.UserJourney
{
    public class UserJourneyValidatorTest
    {
        [Fact]
        public void Validate_ShouldBeTrue_WhenPayloadIsValid()
        {
            //Arrange
            var journeyId = Guid.NewGuid().ToString();
            const int peopleNumber = 3;

            var payload = new UserJourneyValidator
            {
                JourneyId = journeyId,
                PeopleNumber = peopleNumber
            };

            //Act
            var isValid = payload.Validate();

            //Assert
            Assert.True(isValid);
        }

        [Fact]
        public void Validate_ShouldBeFalse_WhenPayloadIsInvalid()
        {
            //Arrange
            var journeyId = "";
            const int peopleNumber = 0;

            var payload = new UserJourneyValidator
            {
                JourneyId = journeyId,
                PeopleNumber = peopleNumber
            };

            //Act
            var isValid = payload.Validate();

            //Assert
            Assert.False(isValid);
        }
    }
}
