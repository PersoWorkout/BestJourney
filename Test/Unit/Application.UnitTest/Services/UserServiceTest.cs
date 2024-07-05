using Application.UnitTest.Fakers.Users;
using Application.Users;
using Application.Users.Customers;
using Domain.Users;
using Domain.Users.Requests;

namespace Application.UnitTest.Services
{
    public class UserServiceTest
    {
        private readonly ICustomerService _userService;
        private readonly IUserRepository _userRepository;

        public UserServiceTest() 
        {
            _userRepository = new FakeUserRepository();

            _userService = new CustomerService(_userRepository, new UpdateCustomerRequestValidator());
        }

        private const string DEFAULT_FIRSTNAME = "John";
        private const string DEFAULT_LASTNAME = "Doe";
        private const string DEFAULT_EMAIL = "john.doe@example.com";
        private const string DEFAULT_PASSWORD = "Password123!";

        [Fact]
        public async void Delete_ShouldBeSuccess_WhenUserExist()
        {
            //Arrange
            var user = await InsertNewUser();

            //Act
            var result = await _userService.Delete(user.Id.ToString());

            //Assert
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void Delete_ShouldBeFailure_WhenIdIsNotAValidGuid()
        {
            //Arrange
            var id = "invalidId";

            //Act
            var result = await _userService.Delete(id);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Equal(UserError.InvalidPayload, result.Error);
        }

        [Fact]
        public async void Delete_ShouldBeFailure_WhenUserNotExist()
        {
            //Arrange
            var id = Guid.Empty.ToString();

            //Act
            var result = await _userService.Delete(id);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Equal(UserError.NotFound, result.Error);
        }

        [Fact]
        public async void GetUsers_ShouldBeSuccess()
        {
            //Arrange
            await InsertNewUser();

            //Act
            var result = await _userService.GetAll();

            //Assert
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void GetById_ShouldBeSuccess_WhenUserExist()
        {
            //Arrange
            var user = await InsertNewUser();

            //Act
            var result = await _userService.GetById(user.Id.ToString());

            //Assert
            Assert.True(result.IsSucess);
            Assert.True(user.Email == result.Data.Email);
        }

        [Fact]
        public async void GetById_ShouldBeFailure_WhenIdIsInvalidGuid()
        {
            //Arrange
            var id = "invalidId";

            //Act
            var result = await _userService.GetById(id);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Equal(UserError.InvalidPayload, result.Error);
        }

        [Fact]
        public async void GetById_ShouldBeFailure_WhenUserNotExist()
        {
            //Arrange
            var id = new Guid();

            //Act
            var result = await _userService.GetById(id.ToString());

            //Assert
            Assert.True(result.IsFailure);
            Assert.Equal(UserError.NotFound, result.Error);
        }

        [Fact]
        public async void Update_SouldBeSuccess_WhenPayloadIsValidAndUserExist()
        {
            //Arrange
            var user = await InsertNewUser();

            const string newFirstname = "Jane";

            var payload = CreatePayloadToUpdate(
                firstname: newFirstname);

            //Act
            var result = await _userService.Update(user.Id.ToString(), payload);

            //Assert
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void Update_SouldBeFailure_WhenIdIsNotAValidGuid()
        {
            //Arrange
            var id = "invalidId";

            var payload = CreatePayloadToUpdate();

            //Act
            var result = await _userService.Update(id, payload);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Equal(UserError.InvalidPayload, result.Error);
        }

        [Fact]
        public async void Update_SouldBeFailure_WhenPayloadIsInvalid()
        {
            //Arrange
            var user = await InsertNewUser();
            var payload = CreatePayloadToUpdate();

            //Act
            var result = await _userService.Update(user.Id.ToString(), payload);

            //Assert
            Assert.Equal(UserError.InvalidPayload, result.Error);
        }

        [Fact]
        public async void Update_SouldBeFailure_WhenUserNotExist()
        {
            //Arrange
            const string newFirstname = "Jane";

            var payload = CreatePayloadToUpdate(
                firstname: newFirstname);

            //Act
            var result = await _userService.Update(Guid.Empty.ToString(), payload);

            //Assert
            Assert.Equal(UserError.NotFound, result.Error);
        }

        [Fact]
        public async void Update_SouldBeFailure_WhenNewEmailIsAlreadyUsed()
        {
            //Arrange
            var user = await InsertNewUser();

            const string newEmail = "jane.doe@example.com";
            await InsertNewUser(email: newEmail);

            var payload = CreatePayloadToUpdate(
                email: newEmail);

            //Act
            var result = await _userService.Update(user.Id.ToString(), payload);

            //Assert
            Assert.Equal(UserError.EmailAlreadyUsed, result.Error);
        }

        private static UpdateCustomerRequest CreatePayloadToUpdate(
            string firstname = "",
            string lastname = "",
            string email = "")
        {
            return new UpdateCustomerRequest
            {
                Firstname = firstname,
                Lastname = lastname,
                Email = email
            };
        }

        private async Task<User> InsertNewUser(
            string firstname = DEFAULT_FIRSTNAME,
            string lastname = DEFAULT_LASTNAME,
            string email = DEFAULT_EMAIL,
            string password = DEFAULT_PASSWORD)
        {
            var user = new User(
                firstname: firstname,
                lastname: lastname,
                email: email,
                password: password,
                "0606060606");

            await _userRepository.Create(user);

            return user;
        }

    }
}
