using Domain.Auth.Requests;

namespace Domain.UnitTest.DTOs.Auth
{
    public class CreateUserValidatorTest
    {
        [Fact]
        public void Validate_ShouldBeValid_WhenPayloadIsValid()
        {
            //Arrange
            var payload = CreateValidPayload();

            //Act
            var result = payload.Validate();

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Valid_ShouldFalse_WhenFirstnameIsInvalid()
        {
            //Arrange
            var payload = CreatePayloadWithInvalidFirstname();

            //Act
            var result = payload.Validate();

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Valid_ShouldFalse_WhenLastnameIsInvalid()
        {
            //Arrange
            var payload = CreatePayloadWithInvalidLastname();

            //Act
            var result = payload.Validate();

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Valid_ShouldFalse_WhenEmailIsInvalid()
        {
            //Arrange
            var payload = CreatePayloadWithInvalidEmail();

            //Act
            var result = payload.Validate();

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Valid_ShouldFalse_WhenPasswordIsInvalid()
        {
            //Arrange
            var payload = CreatePayloadWithInvalidPassword();

            //Act
            var result = payload.Validate();

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Valid_ShouldFalse_WhenPasswordConfirtamionIsInvalid()
        {
            //Arrange
            var payload = CreatePayloadWithoutPasswordValidation();

            //Act
            var result = payload.Validate();

            //Assert
            Assert.False(result);
        }



        private static CreateUserRequest CreateValidPayload()
        {
            return new CreateUserRequest
            {
                Firstname = "John",
                Lastname = "Doe",
                Email = "john.doe@example.com",
                Password = "Password123!",
                PasswordConfirmation = "Password123!"
            };
        }

        private static CreateUserRequest CreatePayloadWithInvalidFirstname()
        {
            return new CreateUserRequest
            {
                Firstname = "",
                Lastname = "Doe",
                Email = "john.doe@example",
                Password = "Password123!",
                PasswordConfirmation = "Password123!"
            };
        }

        private static CreateUserRequest CreatePayloadWithInvalidLastname()
        {
            return new CreateUserRequest
            {
                Firstname = "John",
                Lastname = "",
                Email = "john.doe@example",
                Password = "Password123!",
                PasswordConfirmation = "Password123!"
            };
        }

        private static CreateUserRequest CreatePayloadWithInvalidEmail()
        {
            return new CreateUserRequest
            {
                Firstname = "John",
                Lastname = "Doe",
                Email = "john.doe@example",
                Password = "Password123!",
                PasswordConfirmation = "Password123!"
            };
        }

        private static CreateUserRequest CreatePayloadWithInvalidPassword()
        {
            return new CreateUserRequest
            {
                Firstname = "John",
                Lastname = "Doe",
                Email = "john.doe@example.com",
                Password = "pass",
                PasswordConfirmation = "pass"
            };
        }

        private static CreateUserRequest CreatePayloadWithoutPasswordValidation()
        {
            return new CreateUserRequest
            {
                Firstname = "John",
                Lastname = "Doe",
                Email = "john.doe@example.com",
                Password = "Password123!",
                PasswordConfirmation = "OtherPassword"
            };
        }


    }
}
