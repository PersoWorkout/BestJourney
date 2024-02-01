using Application.Interfaces;
using Application.Interfaces.Auth;
using Application.Interfaces.Users;
using Application.Services;
using Application.UnitTest.Fakers.Auth;
using Application.UnitTest.Fakers.Users;
using AutoMapper;
using Domain.DTOs.Responses;
using Domain.DTOs.Validators.Users;
using Domain.Errors;
using Domain.Models;

namespace Application.UnitTest.Services
{
    public class AuthServiceTest
    {
        private readonly IAuthRepository _authRepository;
        private readonly IAuthService _authService;
        private readonly IHashService _hashService;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public AuthServiceTest()
        {
            _authRepository = new FakeAuthRepository();
            _hashService = new FakeHashService();
            _mapper = FakeUserMapper.Create();
            _tokenService = new FakeTokenService();
            _userRepository = new FakeUserRepository();

            _authService = new AuthService(
                _authRepository, 
                _hashService,
                _mapper,
                _tokenService,
                _userRepository);
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
            var result = await _authService.Register(payload);

            //Assert
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void Create_ShouldReturnAuthenticatedResponse_WhenPayloadIsValid()
        {
            //Arrange
            var payload = CreateValidCreationPayload();

            //Act
            var result = await _authService.Register(payload);

            //Assert
            Assert.IsType<AuthenticatedResponse>(result.Data);
        }

        [Fact]
        public async void Create_ShouldBeFailure_WhenPayloadIsInvalid()
        {
            //Arrange
            var payload = CreateInvalidCreationPayload();

            //Act
            var result = await _authService.Register(payload);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Equal(UserError.InvalidPayload, result.Error);
        }

        [Fact]
        public async void Create_ShouldBeFailure_WhenEmailAlreadyUsed()
        {
            //Arrange
            await CreateUser();
            var secondPayload = CreateValidCreationPayload();

            //Act
            var result = await _authService.Register(secondPayload);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Equal(UserError.EmailAlreadyUsed, result.Error);
        }

        [Fact]
        public async void Login_SouldBeSuccess_WhenLoginSuccessfully()
        {
            //Arrange
            await CreateUser();
            var payload = CreateValidLoginPayload();

            //Act
            var result = await _authService.Login(payload);

            //Assert
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void Login_SouldBeFailure_WhenPayloadIsInvalid()
        {
            //Arrange
            var payload = CreateInvalidLoginPayload();

            //Act
            var result = await _authService.Login(payload);

            //Assert
            Assert.Equal(UserError.InvalidPayload, result.Error);
        }

        [Fact]
        public async void Login_SouldBeFailure_WhenEmailNotExist()
        {
            //Arrange
            var payload = CreateValidLoginPayload(
                email: "unasigned@unasigned.com",
                password: "Password123!");

            //Act
            var result = await _authService.Login(payload);

            //Assert
            Assert.Equal(UserError.InvalidCredentials, result.Error);
        }

        [Fact]
        public async void Login_SouldBeFailure_WhenPasswordNotMatch()
        {
            //Arrange
            var payload = CreateValidLoginPayload(
                password: "Password123!");

            //Act
            var result = await _authService.Login(payload);

            //Assert
            Assert.Equal(UserError.InvalidCredentials, result.Error);
        }

        [Fact]
        public async void ResetPassword_SouldBeSuccess()
        {
            //Arrange
            var user = await CreateUser();
            var payload = CreateResetPasswordPayload();

            //Act
            var result = await _authService.ResetPassword(
                user.Id.ToString(), 
                payload);

            //Assert
            var updatedUser = await _userRepository.GetById(user.Id);
            Assert.True(result.IsSucess);
            Assert.Equal(
                _hashService.Hash(payload.Password), 
                updatedUser.Password);
        }

        [Fact]
        public async void ResetPassword_SouldBeFailure_WhenInvalidPayload()
        {
            //Arrange
            var user = await CreateUser();
            var payload = CreateResetPasswordPayload(
                password: "");

            //Act
            var result = await _authService.ResetPassword(
                user.Id.ToString(),
                payload);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Equal(UserError.InvalidPayload, result.Error);
        }

        [Fact]
        public async void ResetPassword_SouldBeFailure_WhenIdIsInvalidGuid()
        {
            //Arrange
            var userId = "Invalid Guid";
            var payload = CreateResetPasswordPayload();

            //Act
            var result = await _authService.ResetPassword(
                userId,
                payload);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Equal(UserError.NotFound, result.Error);
        }

        [Fact]
        public async void ResetPassword_SouldBeFailure_WhenUserNotExist()
        {
            //Arrange
            var userId = Guid.NewGuid().ToString();
            var payload = CreateResetPasswordPayload();

            //Act
            var result = await _authService.ResetPassword(
                userId,
                payload);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Equal(UserError.NotFound, result.Error);
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
            return new CreateUserValidator();
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

        private static ResetPasswordValidator CreateResetPasswordPayload(
            string password = DEFAULT_PASSWORD + "Secure",
            string passwordConfirmation = DEFAULT_PASSWORD + "Secure")
        {
            return new ResetPasswordValidator
            {
                Password = password,
                PasswordConfirmation = passwordConfirmation
            };
        }

        private async Task<User> CreateUser(
            string firstname = DEFAULT_FIRSTNAME,
            string lastname = DEFAULT_LASTNAME,
            string email = DEFAULT_EMAIL,
            string password = DEFAULT_PASSWORD)
        {
            var user = new User(
                firstname,
                lastname,
                email,
                password);

            await _userRepository.Create(user);

            return user;
        }
    }
}
