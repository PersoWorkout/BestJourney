using Domain.Models;

namespace Domain.UnitTest.Models
{
    public class UserTest
    {
        private const string FIRSTNAME = "John";
        private const string LASTNAME = "Doe";
        private const string EMAIL = "john.doe@example.com";
        private const string PASSWORD = "password";

        [Fact]
        public void CreateUser_ShouldCreateUser()
        {
            //Arrange
            //Act
            var user = InstanceNewUser();

            //Assert
            Assert.True(user.Firstname == FIRSTNAME);
            Assert.True(user.Lastname == LASTNAME);
            Assert.True(user.Email == EMAIL);
            Assert.True(user.Password == PASSWORD);
            Assert.True(user.CreatedAt < DateTime.Now);
        }

        [Fact]
        public void UpdateUser_ShouldUpdateUser()
        {
            //Arrange          
            var user = InstanceNewUser();

            string newFirstname = "Yasin";
            string newLastname = "Karakus";
            string newEmail = "yasin.karakus@example.com";

            //Act
            user.Update(newFirstname, newLastname, newEmail);

            //Assert
            Assert.True(user.Firstname == newFirstname);
            Assert.True(user.Lastname == newLastname);
            Assert.True(user.Email == newEmail);
            Assert.NotNull(user.UpdatedAt);
        }

        private User InstanceNewUser()
        {
            string firstname = FIRSTNAME;
            string lastname = LASTNAME;
            string email = EMAIL;
            string password = PASSWORD;

            return new(firstname, lastname, email, password);
        }
    }
}
