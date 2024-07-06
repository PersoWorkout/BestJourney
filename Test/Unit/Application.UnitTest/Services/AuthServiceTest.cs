using Application.Auth;
using Application.Auth.Validators;
using Application.UnitTest.Fakers.Auth;
using Application.UnitTest.Fakers.Users;
using AutoMapper;
using Domain.Auth;
using Domain.Auth.Requests;
using Domain.Auth.Requests.Customers;
using Domain.DTOs.Responses;
using Domain.Users;

namespace Application.UnitTest.Services
{
    public class AuthServiceTest
    {
        private readonly FakeAuthRepository _authRepository;
        private readonly AuthService _authService;
        private readonly FakeHashService _hashService;
        private readonly IMapper _mapper;
        private readonly FakeTokenService _tokenService;
        private readonly FakeUserRepository _userRepository;

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
                _userRepository,
                new LoginRequestValidator(),
                new RegisterCustomerRequestValidator(),
                new RegisterSupplierRequestValidator());
        }

        private const string DEFAULT_FIRSTNAME = "John";
        private const string DEFAULT_LASTNAME = "Doe";
        private const string DEFAULT_EMAIL = "john.doe@example.com";
        private const string DEFAULT_PASSWORD = "Password123!";

        [Fact]
        public async void Register_ShouldBeSuccess_WhenRegisterSuccessfully()
        {
            //Arrange
            var payload = CreateValidCreationPayload();

            //Act
            var result = await _authService.RegisterCustomer(payload);

            //Assert
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void Register_ShouldReturnAuthenticatedResponse_WhenRegisterSuccessfully()
        {
            //Arrange
            var payload = CreateValidCreationPayload();

            //Act
            var result = await _authService.RegisterCustomer(payload);

            //Assert
            Assert.IsType<AuthenticatedResponse>(result.Data);
        }

        [Fact]
        public async void Register_ShouldBeFailure_WhenPayloadIsInvalid()
        {
            //Arrange
            var payload = CreateInvalidCreationPayload();

            //Act
            var result = await _authService.RegisterCustomer(payload);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Equal(UserError.InvalidPayload, result.Error);
        }

        [Fact]
        public async void Register_ShouldBeFailure_WhenEmailAlreadyUsed()
        {
            //Arrange
            await CreateUser();
            var secondPayload = CreateValidCreationPayload();

            //Act
            var result = await _authService.RegisterCustomer(secondPayload);

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
            var result = await _authService.LoginCustomer(payload);

            //Assert
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void Login_SouldReturnAuthenticatedResponse_WhenLoginSuccessfully()
        {
            //Arrange
            await CreateUser();
            var payload = CreateValidLoginPayload();

            //Act
            var result = await _authService.LoginCustomer(payload);

            //Assert
            Assert.IsType<AuthenticatedResponse>(result.Data);
        }

        [Fact]
        public async void Login_SouldBeFailure_WhenPayloadIsInvalid()
        {
            //Arrange
            var payload = CreateInvalidLoginPayload();

            //Act
            var result = await _authService.LoginCustomer(payload);

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
            var result = await _authService.LoginCustomer(payload);

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
            var result = await _authService.LoginCustomer(payload);

            //Assert
            Assert.Equal(UserError.InvalidCredentials, result.Error);
        }

        [Fact]
        public async void ResetPassword_SouldBeSuccess_WhenResetPasswordSuccessfully()
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

        [Fact]
        public async void IsAuthenticated_ShouldBeReturnAuthenticatedUserId_WhenUserIsAuthenticated()
        {
            //Arrange
            var user = await CreateUser();

            var token = Guid.NewGuid().ToString();

            CreateSession(
                user.Id.ToString(), 
                token);

            //Act
            var result = await _authService.IsAuthenticated(token);

            //Assert
            Assert.Equal(user.Id, result?.Id);
            
        }

        [Fact]
        public async void IsAuthenticated_ShouldBeReturnNull_WhenTokenIsNull()
        {
            //Arrange
            string? token = string.Empty;

            //Act
            var result = await _authService.IsAuthenticated(token);

            //Assert
            Assert.Null(result);

        }

        [Fact]
        public async void IsAuthenticated_ShouldBeReturnNull_WhenUserNotAuthenticated()
        {
            //Arrange
            string? token = Guid.NewGuid().ToString();

            //Act
            var result = await _authService.IsAuthenticated(token);

            //Assert
            Assert.Null(result);

        }

        private static RegisterCustomerRequest CreateValidCreationPayload(
            string firstname = DEFAULT_FIRSTNAME,
            string lastname = DEFAULT_LASTNAME,
            string email = DEFAULT_EMAIL,
            string password = DEFAULT_PASSWORD)
        {
            return new RegisterCustomerRequest
            {
                Firstname = firstname,
                Lastname = lastname,
                Email = email,
                Password = password,
                PasswordConfirmation = password,
                Phone = "0606060606"
            };
        }

        private static RegisterCustomerRequest CreateInvalidCreationPayload()
        {
            return new RegisterCustomerRequest
            {
                Firstname = "",
                Lastname = "",
                Email = "",
                Password = "",
                PasswordConfirmation= "",
                Phone = ""
            };
        }

        private static LoginRequest CreateValidLoginPayload(
            string email = DEFAULT_EMAIL,
            string password = DEFAULT_PASSWORD)
        {
            return new LoginRequest
            {
                Email = email,
                Password = password,
            };
        }

        private static LoginRequest CreateInvalidLoginPayload()
        {
            return new LoginRequest
            {
                Email = "john.doe",
                Password = "pass",
            };
        }

        private static ResetPasswordRequest CreateResetPasswordPayload(
            string password = DEFAULT_PASSWORD + "Secure",
            string passwordConfirmation = DEFAULT_PASSWORD + "Secure")
        {
            return new ResetPasswordRequest
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
                password,
                "0606060606");

            await _userRepository.Create(user);

            return user;
        }

        private void CreateSession(string userId, string token)
        {
            _authRepository._tokens.Add(new TokenDTO(token, userId));
        }
    }
}
