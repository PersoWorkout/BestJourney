using Domain.Auth.Requests.Customers;

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



        private static RegisterCustomerRequest CreateValidPayload()
        {
            return new RegisterCustomerRequest
            {
                Firstname = "John",
                Lastname = "Doe",
                Email = "john.doe@example.com",
                Password = "Password123!",
                PasswordConfirmation = "Password123!"
            };
        }

        private static RegisterCustomerRequest CreatePayloadWithInvalidFirstname()
        {
            return new RegisterCustomerRequest
            {
                Firstname = "",
                Lastname = "Doe",
                Email = "john.doe@example",
                Password = "Password123!",
                PasswordConfirmation = "Password123!"
            };
        }

        private static RegisterCustomerRequest CreatePayloadWithInvalidLastname()
        {
            return new RegisterCustomerRequest
            {
                Firstname = "John",
                Lastname = "",
                Email = "john.doe@example",
                Password = "Password123!",
                PasswordConfirmation = "Password123!"
            };
        }

        private static RegisterCustomerRequest CreatePayloadWithInvalidEmail()
        {
            return new RegisterCustomerRequest
            {
                Firstname = "John",
                Lastname = "Doe",
                Email = "john.doe@example",
                Password = "Password123!",
                PasswordConfirmation = "Password123!"
            };
        }

        private static RegisterCustomerRequest CreatePayloadWithInvalidPassword()
        {
            return new RegisterCustomerRequest
            {
                Firstname = "John",
                Lastname = "Doe",
                Email = "john.doe@example.com",
                Password = "pass",
                PasswordConfirmation = "pass"
            };
        }

        private static RegisterCustomerRequest CreatePayloadWithoutPasswordValidation()
        {
            return new RegisterCustomerRequest
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
