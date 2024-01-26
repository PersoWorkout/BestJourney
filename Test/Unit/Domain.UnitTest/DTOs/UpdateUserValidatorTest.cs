using Domain.DTOs.Validators.Users;

namespace Domain.UnitTest.DTOs
{
    public class UpdateUserValidatorTest
    {
        [Fact]
        public void Validate_ShouldBeTrue_WhenPayloadIsValid()
        {
            //Arrange
            var payload = CreateValidPayload();

            //Act
            var result = payload.Validate();

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_ShouldBeFalse_WhenPayloadIsEmpty()
        {
            //Arrange
            var payload = CreateInvalidPayload();

            //Act
            var result = payload.Validate();

            //Assert
            Assert.False(result);
        }

        private static UpdateUserValidator CreateValidPayload()
        {
            return new UpdateUserValidator
            {
                Firstname = "John",
                Lastname = "Doe",
                Email = "john.doe@example.com",
                Password = "Password123!"
            };
        }

        private static UpdateUserValidator CreateInvalidPayload()
        {
            return new UpdateUserValidator();
        }
    }
}
