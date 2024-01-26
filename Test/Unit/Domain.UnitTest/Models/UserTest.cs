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
            Assert.True(ValidateUserCreation(user));
        }

        [Fact]
        public void UpdateUser_ShouldUpdateUser()
        {
            //Arrange          
            var user = InstanceNewUser();

            string newFirstname = "Yasin";
            string newLastname = "Karakus";
            string newEmail = "yasin.karakus@example.com";
            string password = "Password123!";

            //Act
            user.Update(newFirstname, newLastname, newEmail, password);

            //Assert
            Assert.True(ValidateUserUpdate(user, newFirstname, newLastname, newEmail));
        }

        private static User InstanceNewUser()
        {
            string firstname = FIRSTNAME;
            string lastname = LASTNAME;
            string email = EMAIL;
            string password = PASSWORD;

            return new(firstname, lastname, email, password);
        }

        private static bool ValidateUserCreation(User user)
        {
            return user.Firstname == FIRSTNAME &&
                user.Lastname == LASTNAME &&
                user.Email == EMAIL &&
                user.Password == PASSWORD &&
                user.CreatedAt < DateTime.Now;
        }

        private static bool ValidateUserUpdate(User user, string firstname, string lastname, string email)
        {
            return user.Firstname == firstname &&
                user.Lastname == lastname &&
                user.Email == email &&
                user.UpdatedAt.HasValue;
        }
    }
}
