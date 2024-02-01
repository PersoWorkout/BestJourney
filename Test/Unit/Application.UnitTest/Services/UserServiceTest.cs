using Application.Interfaces.Users;
using Application.Services;
using Application.UnitTest.Fakers.Users;
using Domain.DTOs.Validators.Users;
using Domain.Errors;
using Domain.Models;

namespace Application.UnitTest.Services
{
    public class UserServiceTest
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;

        public UserServiceTest() 
        {
            _userRepository = new FakeUserRepository();
            var mapper = FakeUserMapper.Create();

            _userService = new UserService(_userRepository, mapper);
        }

        private const string DEFAULT_FIRSTNAME = "John";
        private const string DEFAULT_LASTNAME = "Doe";
        private const string DEFAULT_EMAIL = "john.doe@example.com";
        private const string DEFAULT_PASSWORD = "Password123!";

        //[Fact]
        //public async void Create_ShouldBeSuccess_WhenPayloadIsValid()
        //{
        //    //Arrange
        //    var payload = CreateValidCreationPayload();

        //    //Act
        //    var result = await _userService.Create(payload);

        //    //Assert
        //    Assert.True(result.IsSucess);
        //}

        //[Fact]
        //public async void Create_ShouldBeFailure_WhenPayloadIsInvalid()
        //{
        //    //Arrange
        //    var payload = CreateInvalidCreationPayload();

        //    //Act
        //    var result = await _userService.Create(payload);

        //    //Assert
        //    Assert.True(result.IsFailure);
        //    Assert.Equal(UserError.InvalidPayload, result.Error);
        //}

        //[Fact]
        //public async void Create_ShouldBeFailure_WhenEmailAlreadyUsed()
        //{
        //    //Arrange
        //    var firstPayload = CreateValidCreationPayload();
        //    var firstResult = await _userService.Create(firstPayload);

        //    if (firstResult.IsFailure) Assert.Fail("Error during first creation");


        //    var secondPayload = CreateValidCreationPayload();

        //    //Act
        //    var result = await _userService.Create(secondPayload);

        //    //Assert
        //    Assert.True(result.IsFailure);
        //    Assert.Equal(UserError.EmailAlreadyUsed, result.Error);
        //}

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
            var result = await _userService.GetUsers();

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

        //[Fact]
        //public async void Login_SouldBeSuccess_WhenLoginSuccessfully()
        //{
        //    //Arrange
        //    var createPayload = CreateValidCreationPayload();
        //    var firstResult = await _userService.Create(createPayload);

        //    if (firstResult.IsFailure) Assert.Fail("Error during first creation");

        //    var loginPayload = CreateValidLoginPayload();

        //    //Act
        //    var result = await _userService.Login(loginPayload);

        //    //Assert
        //    Assert.True(result.IsSucess);
        //}

        //[Fact]
        //public async void Login_SouldBeFailure_WhenPayloadIsInvalid()
        //{
        //    //Arrange
        //    var loginPayload = CreateInvalidLoginPayload();

        //    //Act
        //    var result = await _userService.Login(loginPayload);

        //    //Assert
        //    Assert.Equal(UserError.InvalidPayload, result.Error);
        //}

        //[Fact]
        //public async void Login_SouldBeFailure_WhenEmailNotExist()
        //{
        //    //Arrange
        //    var loginPayload = CreateValidLoginPayload(
        //        "unasigned@unasigned.com", 
        //        "Password123!");

        //    //Act
        //    var result = await _userService.Login(loginPayload);

        //    //Assert
        //    Assert.Equal(UserError.InvalidCredentials, result.Error);
        //}

        //[Fact]
        //public async void Login_SouldBeFailure_WhenPasswordNotMatch()
        //{
        //    //Arrange
        //    var loginPayload = CreateValidLoginPayload(
        //        password: "Password123!");

        //    //Act
        //    var result = await _userService.Login(loginPayload);

        //    //Assert
        //    Assert.Equal(UserError.InvalidCredentials, result.Error);
        //}

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

        //private static CreateUserValidator CreateValidCreationPayload(
        //    string firstname = DEFAULT_FIRSTNAME,
        //    string lastname = DEFAULT_LASTNAME,
        //    string email = DEFAULT_EMAIL,
        //    string password = DEFAULT_PASSWORD)
        //{
        //    return new CreateUserValidator
        //    {
        //        Firstname = firstname,
        //        Lastname = lastname,
        //        Email = email,
        //        Password = password,
        //        PasswordConfirmation = password
        //    };
        //}

        //private static CreateUserValidator CreateInvalidCreationPayload()
        //{
        //    return new CreateUserValidator
        //    {
        //        Firstname = DEFAULT_FIRSTNAME,
        //        Lastname = string.Empty,
        //        Email = DEFAULT_EMAIL,
        //        Password = DEFAULT_PASSWORD,
        //        PasswordConfirmation = DEFAULT_PASSWORD
        //    };
        //}

        //private static LoginUserValidator CreateValidLoginPayload(
        //    string email = DEFAULT_EMAIL,
        //    string password = DEFAULT_PASSWORD)
        //{
        //    return new LoginUserValidator
        //    {
        //        Email = email,
        //        Password = password,
        //    };
        //}

        //private static LoginUserValidator CreateInvalidLoginPayload()
        //{
        //    return new LoginUserValidator
        //    {
        //        Email = "john.doe",
        //        Password = "pass",
        //    };
        //}

        private static UpdateUserValidator CreatePayloadToUpdate(
            string firstname = "",
            string lastname = "",
            string email = "")
        {
            return new UpdateUserValidator
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
                password: password);

            await _userRepository.Create(user);

            return user;
        }

    }
}
