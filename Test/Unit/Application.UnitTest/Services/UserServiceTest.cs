using Application.Interfaces.Users;
using Application.Services;
using Application.UnitTest.Fakers;
using Domain.DTOs.Validators.Users;
using Domain.Errors.Users;

namespace Application.UnitTest.Services
{
    public class UserServiceTest
    {
        private readonly IUserService _userService;

        public UserServiceTest() 
        {
            var userRepository = new FakeUserRepository();
            var mapper = FakeUserMapper.Create();
            var hashService = new FakeHashService();

            _userService = new UserService(userRepository, mapper, hashService);
        }

        private const string DEFAULT_FIRSTNAME = "John";
        private const string DEFAULT_LASTNAME = "Doe";
        private const string DEFAULT_EMAIL = "john.doe@example.com";
        private const string DEFAULT_PASSWORD = "Password123!";

        [Fact]
        public async void Create_ShouldBeSuccess_WhenPayloadIsValid()
        {
            //Arrange
            var payload = CreateValidCreationPayload();

            //Act
            var result = await _userService.Create(payload);

            //Assert
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void Create_ShouldBeFailure_WhenPayloadIsInvalid()
        {
            //Arrange
            var payload = CreateInvalidCreationPayload();

            //Act
            var result = await _userService.Create(payload);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Equal(UserError.InvalidPayload, result.Error);
        }

        [Fact]
        public async void Create_ShouldBeFailure_WhenEmailAlreadyUsed()
        {
            //Arrange
            var firstPayload = CreateValidCreationPayload();
            var firstResult = await _userService.Create(firstPayload);

            if (firstResult.IsFailure) Assert.Fail("Error during first creation");


            var secondPayload = CreateValidCreationPayload();

            //Act
            var result = await _userService.Create(secondPayload);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Equal(UserError.EmailAlreadyUsed, result.Error);
        }

        [Fact]
        public async void Delete_ShouldBeSuccess_WhenUserExist()
        {
            //Arrange
            var firstPayload = CreateValidCreationPayload();
            var firstResult = await _userService.Create(firstPayload);

            if (firstResult.IsFailure) Assert.Fail("Error during first creation");

            //Act
            var result = await _userService.Delete(firstResult.Data!.Id);

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
            var firstPayload = CreateValidCreationPayload();
            await _userService.Create(firstPayload);

            //Act
            var result = await _userService.GetUsers();

            //Assert
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void GetById_ShouldBeSuccess_WhenUserExist()
        {
            //Arrange
            var firstPayload = CreateValidCreationPayload();
            var firstResult = await _userService.Create(firstPayload);

            //Act
            var result = await _userService.GetById(firstResult.Data!.Id);

            //Assert
            Assert.True(result.IsSucess);
            Assert.True(firstResult.Data.Email == result.Data.Email);
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
        public async void Login_SouldBeSuccess_WhenLoginSuccessfully()
        {
            //Arrange
            var createPayload = CreateValidCreationPayload();
            var firstResult = await _userService.Create(createPayload);

            if (firstResult.IsFailure) Assert.Fail("Error during first creation");

            var loginPayload = CreateValidLoginPayload();

            //Act
            var result = await _userService.Login(loginPayload);

            //Assert
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void Login_SouldBeFailure_WhenPayloadIsInvalid()
        {
            //Arrange
            var loginPayload = CreateInvalidLoginPayload();

            //Act
            var result = await _userService.Login(loginPayload);

            //Assert
            Assert.Equal(UserError.InvalidPayload, result.Error);
        }

        [Fact]
        public async void Login_SouldBeFailure_WhenEmailNotExist()
        {
            //Arrange
            var loginPayload = CreateValidLoginPayload(
                "unasigned@unasigned.com", 
                "Password123!");

            //Act
            var result = await _userService.Login(loginPayload);

            //Assert
            Assert.Equal(UserError.InvalidCredentials, result.Error);
        }

        [Fact]
        public async void Login_SouldBeFailure_WhenPasswordNotMatch()
        {
            //Arrange
            var loginPayload = CreateValidLoginPayload(
                password: "Password123!");

            //Act
            var result = await _userService.Login(loginPayload);

            //Assert
            Assert.Equal(UserError.InvalidCredentials, result.Error);
        }

        [Fact]
        public async void Update_SouldBeSuccess_WhenPayloadIsValidAndUserExist()
        {
            //Arrange
            var createPayload = CreateValidCreationPayload();
            var firstResult = await _userService.Create(createPayload);

            if (firstResult.IsFailure) Assert.Fail("Error during first creation");

            const string newFirstname = "Jane";

            var payload = CreatePayloadToUpdate(
                firstname: newFirstname);

            //Act
            var result = await _userService.Update(firstResult.Data!.Id, payload);

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
            var createPayload = CreateValidCreationPayload();
            var firstResult = await _userService.Create(createPayload);

            if (firstResult.IsFailure) Assert.Fail("Error during first creation");

            var payload = CreatePayloadToUpdate();

            //Act
            var result = await _userService.Update(firstResult.Data!.Id, payload);

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
            var firstCreatePayload = CreateValidCreationPayload();
            var firstResult = await _userService.Create(firstCreatePayload);
            if (firstResult.IsFailure) Assert.Fail("Error during first creation");

            const string newEmail = "jane.doe@example.com";

            var secondCreatePayload = CreateValidCreationPayload(
                email: newEmail);
            var secondResult = await _userService.Create(secondCreatePayload);
            if (secondResult.IsFailure) Assert.Fail("Error during first creation");

            var payload = CreatePayloadToUpdate(
                email: newEmail);

            //Act
            var result = await _userService.Update(firstResult.Data!.Id, payload);

            //Assert
            Assert.Equal(UserError.EmailAlreadyUsed, result.Error);
        }

        private static CreateUserValidator CreateValidCreationPayload(
            string firstname = DEFAULT_FIRSTNAME,
            string lastname = DEFAULT_LASTNAME,
            string email = DEFAULT_EMAIL,
            string password = DEFAULT_PASSWORD)
        {
            return new CreateUserValidator
            {
                Firstname = firstname,
                Lastname = lastname,
                Email = email,
                Password = password,
                PasswordConfirmation = password
            };
        }

        private static CreateUserValidator CreateInvalidCreationPayload()
        {
            return new CreateUserValidator
            {
                Firstname = DEFAULT_FIRSTNAME,
                Lastname = string.Empty,
                Email = DEFAULT_EMAIL,
                Password = DEFAULT_PASSWORD,
                PasswordConfirmation = DEFAULT_PASSWORD
            };
        }

        private static LoginUserValidator CreateValidLoginPayload(
            string email = DEFAULT_EMAIL,
            string password = DEFAULT_PASSWORD)
        {
            return new LoginUserValidator
            {
                Email = email,
                Password = password,
            };
        }

        private static LoginUserValidator CreateInvalidLoginPayload()
        {
            return new LoginUserValidator
            {
                Email = "john.doe",
                Password = "pass",
            };
        }

        private static UpdateUserValidator CreatePayloadToUpdate(
            string firstname = "",
            string lastname = "",
            string email = "",
            string password = "")
        {
            return new UpdateUserValidator
            {
                Firstname = firstname,
                Lastname = lastname,
                Email = email,
                Password = password
            };
        }

    }
}
