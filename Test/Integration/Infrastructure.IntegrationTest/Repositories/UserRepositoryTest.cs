using Application.Interfaces.Users;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Integrationtest.Repositories
{
    public class UserRepositoryTest: 
        BaseIntegrationTest
    {

        protected readonly IUserRepository _userRepository;

        public UserRepositoryTest(WebApplicationFactory factory):
            base(factory)
        {
            _userRepository = _scope.ServiceProvider
                .GetRequiredService<IUserRepository>();
        }

        private const string FIRSTNAME = "John";
        private const string LASTNAME = "Doe";
        private const string EMAIL = "john.doe@example.com";
        private const string PASSWORD = "Password123!";


        [Fact]
        public async void GetUsers_ShouldReturnAListOfUser()
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
