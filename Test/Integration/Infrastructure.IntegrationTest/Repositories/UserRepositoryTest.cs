using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Integrationtest.Repositories
{
    public class UserRepositoryTest(WebApplicationFactory factory): 
        BaseIntegrationTest(factory)
    {
        private const string FIRSTNAME = "John";
        private const string LASTNAME = "Doe";
        private const string EMAIL = "john.doe@example.com";
        private const string PASSWORD = "Password123!";

        ////Move to unit test
        //[Fact]
        //public async void CheckPassword_ShouldBeTrue_WhenPasswordsAreIdentic()
        //{
        //    //Arrange
        //    var user = await CreateUser();
        //    if (user == null)
        //    {
        //        Assert.Fail("Error when create a new user");
        //        return;
        //    }

        //    //Act
        //    var result = _userRepository.CheckPassword(user, PASSWORD);

        //    //Assert
        //    Assert.True(result);
        //    await ClearUser();
        //}

        ////Move to unit test
        //[Fact]
        //public async void CheckPassword_ShouldBeFalse_WhenPasswordsAreDifferent()
        //{
        //    //Arrange
        //    var user = await CreateUser();
        //    if (user == null)
        //    {
        //        Assert.Fail("Error when create a new user");
        //        return;
        //    }

        //    //Act
        //    var result = _userRepository.CheckPassword(user, "123Password!");

        //    //Assert
        //    Assert.False(result);
        //    await ClearUser();
        //}

        ////Move to unit test
        //[Fact]
        //public void HashPassword_SoulBeTrue_WhenPasswordHasBeenHashed()
        //{
        //    //Arrange
        //    var user = new User(FIRSTNAME, LASTNAME, EMAIL, PASSWORD);

        //    //Act
        //    _userRepository.HashPassword(user);

        //    //Assert
        //    Assert.True(
        //        !string.IsNullOrEmpty(user.Password) && 
        //        user.Password != PASSWORD);

        //}

        [Fact]
        public async void GetUsers_ShouldBeReturnAListOfUser()
        {
            //Arrange
            await CreateUser();

            //Act
            var result = await _userRepository.GetUsers();

            //Assert
            Assert.NotEmpty(result);
            ClearUser();
        }

        [Fact]
        public async void Create_ShouldBeTrue_WhenUserHasBeenCreated()
        {
            //Arrange
            var userPaylaod = new User(
                FIRSTNAME,
                LASTNAME,
                EMAIL,
                PASSWORD);

            //Act
            var user = await _userRepository.Create(userPaylaod);

            //Assert
            var userInDb = 
                await _dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            Assert.NotNull(userInDb);
            await ClearUser();
        }

        [Fact]
        public async void Delete_ShouldBeTrue_WhenUserHasBeenDeleted()
        {
            //Arrange
            var user = await CreateUser();
            if (user == null)
            {
                Assert.Fail("Error when create a new user");
                return;
            }

            //Act
            await _userRepository.Delete(user);

            //Assert
            var userDeleted = 
                !await _dbContext
                .Users
                .AnyAsync(u => u.Id == user.Id);

            Assert.True(userDeleted);
            await ClearUser();
        }

        [Fact]
        public async void GetByEmail_ShouldBeTrue_WhenEmailEqualToExpectedEmail()
        {
            //Arrange
            var createdUser = await CreateUser();
            if (createdUser == null)
            {
                Assert.Fail("Error when create a new user");
                return;
            }

            //Act
            var user = await _userRepository.GetByEmail(createdUser.Email);
            if(user == null)
            {
                Assert.Fail("Error when fetch user by email");
                return;
            }

            //Assert
            Assert.Equal(createdUser.Email, user.Email);
            Assert.Equal(createdUser.Id, user.Id);
            await ClearUser();
        }

        [Fact]
        public async void GetById_ShouldBeTrue_WhenUserWithExpectedIdExist()
        {
            //Arrange
            var createdUser = await CreateUser();
            if (createdUser == null)
            {
                Assert.Fail("Error when create a new user");
                return;
            }

            //Act
            var user = await _userRepository.GetById(createdUser.Id);

            //Assert
            Assert.NotNull(user);
            Assert.Equal(createdUser.Id, user.Id);
            await ClearUser();
        }

        [Fact]
        public async void Update_ShouldUpdate_WhenUserHasBeenUpdated()
        {
            //Arrange
            var user = await CreateUser();
            if (user == null)
            {
                Assert.Fail("Error when create a new user");
                return;
            }

            //Act
            user.Update(
                "Jane", 
                string.Empty, 
                "jane.doe@example.com", 
                string.Empty);

            user = await _userRepository.Update(user);

            //Assert
            Assert.NotNull(user);

            var updatedUser =
                await _dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            Assert.NotNull(updatedUser);
            Assert.Equal(updatedUser.Id, user.Id);
            Assert.Equal("Jane", updatedUser.Firstname);
            await ClearUser();
        }

        private async Task<User> CreateUser()
        {
            var user = new User(
                FIRSTNAME, 
                LASTNAME, 
                EMAIL, 
                BCrypt.Net.BCrypt.HashPassword(PASSWORD));

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        private async Task ClearUser()
        {
            _dbContext.Users.RemoveRange(_dbContext.Users.ToList());
            await _dbContext.SaveChangesAsync();
        }
    }
}
