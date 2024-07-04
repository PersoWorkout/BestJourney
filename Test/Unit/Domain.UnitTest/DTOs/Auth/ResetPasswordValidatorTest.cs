using Domain.Auth.Requests;

namespace Domain.UnitTest.DTOs.Auth
{
    public class ResetPasswordValidatorTest
    {
        [Fact]
        public void Validate_ShouldBeTrue_WhenPayloadIsValid()
        {
            //Arrange
            var payload = new ResetPasswordRequest
            {
                Password = "Password132!",
                PasswordConfirmation = "Password132!",
            };

            //Act
            var result = payload.Validate();

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_ShouldBeFalse_WhenNotValidPassword()
        {
            //Arrange
            var payload = new ResetPasswordRequest
            {
                Password = "pass",
                PasswordConfirmation = "pass",
            };

            //Act
            var result = payload.Validate();

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_ShouldBeFalse_WhenConfirmationANdPasswordAreNotIdentic()
        {
            //Arrange
            var payload = new ResetPasswordRequest
            {
                Password = "Password132!",
                PasswordConfirmation = "OtherPassword132!",
            };

            //Act
            var result = payload.Validate();

            //Assert
            Assert.False(result);
        }
    }
}
