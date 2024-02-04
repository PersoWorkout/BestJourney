using Domain.Enums;
using Domain.Models;

namespace Domain.UnitTest.Models
{
    public class UserJourneyTest
    {
        [Fact]
        public void CreateUserJourney_ShouldBeCreateUserJourney()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
            Guid journeyId = Guid.NewGuid();
            const int peopleNumber = 3;

            //Act
            var userJourney = CreateUserJourney(peopleNumber, userId, journeyId);

            //Assert
            Assert.True(userJourney.UserId == userId &&
                userJourney.JourneyId == journeyId &&
                userJourney.PeopleNumber == peopleNumber &&
                userJourney.Status == PaymentStatus.Unpaied);
        }

        [Fact]
        public void Update_ShouldUpdateOnlyPeopleNumber()
        {
            //Arrange
            var userJourney = CreateUserJourney(2);

            //Act
            userJourney.Update(3);

            //Assert
            Assert.True(userJourney.PeopleNumber == 3 &&
                userJourney.Status == PaymentStatus.Unpaied); ;
        }

        [Fact]
        public void Update_ShouldUpdateOnlyStatus()
        {
            //Arrange
            var userJourney = CreateUserJourney(2);

            //Act
            userJourney.Update(status: PaymentStatus.Paied);

            //Assert
            Assert.True(userJourney.PeopleNumber == 2 &&
                userJourney.Status == PaymentStatus.Paied); ;
        }

        private static UserJourney CreateUserJourney(
            int peopleNumber,
            Guid? userId = null,
            Guid? journeyId = null)
        {
            return new UserJourney(
                userId ?? Guid.NewGuid(),
                journeyId ?? Guid.NewGuid(),
                peopleNumber);
        }
    }
}
