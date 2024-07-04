using Domain.Auth.Requests;

namespace Domain.UnitTest.DTOs.Auth
{
    public class LoginUserValidatorTest
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
        public void Validate_ShouldBeFalse_WhenEmailIsInvalid()
        {
            //Arrange
            var payload = CreatePayloadWithInvalidEmail();

            //Act
            var result = payload.Validate();

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_ShouldBeFalse_WhenPasswordIsInvalid()
        {
            //Arrange
            var payload = CreatePayloadWithInvalidPassword();

            //Act
            var result = payload.Validate();

            //Assert
            Assert.False(result);
        }

        private static LoginUserRequest CreateValidPayload()
        {
            return new LoginUserRequest
            {
                Email = "john.doe@example.com",
                Password = "Password123!"
            };
        }

        private static LoginUserRequest CreatePayloadWithInvalidEmail()
        {
            return new LoginUserRequest
            {
                Email = "johndoeexample.com",
                Password = "Password123!"
            };
        }

        private static LoginUserRequest CreatePayloadWithInvalidPassword()
        {
            return new LoginUserRequest
            {
                Email = "john.doe@example.com",
                Password = "pass"
            };
        }
    }
}
