using Infrastructure.Services;

namespace Infrastructure.UnitTest.Services
{
    public class HashServiceTest
    {
        private readonly HashService _hashService = new();
        private const string PASSWORD = "Password123!";

        [Fact]
        public void Hash_ShouldHashPassword_WhenPasswordIsValid()
        {
            //Arrange
            var password = PASSWORD;

            //Act
            var result = _hashService.Hash(password);

            //Assert
            Assert.NotEqual(password, result);
        }

        [Fact]
        public void Hash_ShouldReturnEmptyString_WhenPasswordIsNull()
        {
            //Arrange
            string? password = null;

            //Act
            var result = _hashService.Hash(password);

            //Assert
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void Verify_ShouldBeTrue_WhenPasswordsAreIdentic()
        {
            //Arrange
            var password = PASSWORD;
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            //Act
            var result = _hashService.Verify(password, hashedPassword);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Verify_ShouldBeFalse_WhenPasswordsAreDifferent()
        {
            //Arrange
            var password = PASSWORD;
            var differentPassword = PASSWORD + "Test";
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            //Act
            var result = _hashService.Verify(differentPassword, hashedPassword);

            //Assert
            Assert.False(result);
        }
    }
}
