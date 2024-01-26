

using Domain.Utils;

namespace Domain.UnitTest.Utils.Test
{
    public class StringValidationTest
    {
        [Fact]
        public void IsValid_ShouldBeTrue_WhenValueIsValid()
        {
            //Arrange
            string value = LoremIpsum.Generate();

            //Act
            var result = value.IsValid();

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValid_ShouldBeFalse_WhenValueIsNull()
        {
            //Arrange
            string? value = null;

            //Act
            var result = value.IsValid();

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValid_ShouldBeFalse_WhenValueIsEmpty()
        {
            //Arrange
            string value = string.Empty;

            //Act
            var result = value.IsValid();

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValid_ShouldBeFalse_WhenValueIsWhiteSpace()
        {
            //Arrange
            string value = " ";

            //Act
            var result = value.IsValid();

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValidEmail_ShouldBeTrue_WhenEmailIsValid()
        {
            //Arrange
            string value = "john.doe@example.com";

            //Act
            var result = value.IsValidEmail();

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidEmail_ShouldBeFalse_WhenEmailIsNotValid()
        {
            //Arrange
            string value = "john.doe";

            //Act
            var result = value.IsValidEmail();

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValidPassword_ShouldBeTrue_WhenPasswordIsValid()
        {
            //Arrange
            string value = "Password123!";

            //Act
            var result = value.IsValidPassword();

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidPassword_ShouldBeFalse_WhenPasswordIsTooShort()
        {
            //Arrange
            string value = "pass";

            //Act
            var result = value.IsValidPassword();

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValidPassword_ShouldBeFalse_WhenPasswordIsInvalid()
        {
            //Arrange
            string value = "password";

            //Act
            var result = value.IsValidPassword();

            //Assert
            Assert.False(result);
        }

    }
}
