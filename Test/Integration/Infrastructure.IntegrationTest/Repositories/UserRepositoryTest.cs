using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IntegrationTest.Repositories
{
    public class UserRepositoryTest(WebApplicationFactory factory): 
        BasicIntegrationTest(factory)
    {
        private const string FIRSTNAME = "John";
        private const string LASTNAME = "Doe";
        private const string EMAIL = "john.doe@example.com";
        private const string PASSWORD = "Password123!";

        [Fact]
        public async void Create_ShouldCreateUser()
        {
            //Arrange
            var user = CreateUser();

            //Act
            var createdsUser = await _userRepository.Create(user);

            //Assert
            var resultInDb = _dbContext.Users.FirstOrDefault(u => u.Id == createdsUser.Id);
            Assert.NotNull(resultInDb);
        }

        private static User CreateUser()
        {
            return new User(FIRSTNAME, LASTNAME, EMAIL, PASSWORD);
        }
    }
}
