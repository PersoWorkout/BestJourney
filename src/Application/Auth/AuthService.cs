using Application.Users;
using AutoMapper;
using Domain.Abstractions;
using Domain.Auth;
using Domain.Auth.Requests;
using Domain.Auth.Requests.Customers;
using Domain.Auth.Requests.Suppliers;
using Domain.DTOs.Responses;
using Domain.Users;
using FluentValidation;

namespace Application.Auth;

public class AuthService(
    IAuthRepository authRepository,
    IHashService hashService,
    IMapper mapper,
    ITokenService tokenService,
    IUserRepository userRepository,
    IValidator<LoginRequest> loginValidator,
    IValidator<RegisterCustomerRequest> registerCustomerValidator,
    IValidator<RegisterSupplierRequest> registerSupplierValidator) : IAuthService
{
    private readonly IAuthRepository _authRepository = authRepository;
    private readonly IHashService _hashService = hashService;
    private readonly IMapper _mapper = mapper;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IValidator<LoginRequest> _loginValidator = loginValidator;
    private readonly IValidator<RegisterCustomerRequest> _registerCustomerValidator = registerCustomerValidator;
    private readonly IValidator<RegisterSupplierRequest> _registerSupplierValidator = registerSupplierValidator;

    public async Task<Result<AuthenticatedResponse>> RegisterSupplier(RegisterSupplierRequest payload)
    {
        var validation = _registerSupplierValidator.Validate(payload);
        if(!validation.IsValid)
            return Result<AuthenticatedResponse>.Failure(UserError.InvalidPayload);

        if (await _userRepository.GetSupplierByEmail(payload.Email) is not null)
            return Result<AuthenticatedResponse>.Failure(UserError.EmailAlreadyUsed);

        var hashedPassword = _hashService.Hash(payload.Password);
        if (string.IsNullOrEmpty(hashedPassword))
            return Result<AuthenticatedResponse>.Failure(UserError.InvalidPayload);

        var user = new User(
            firstname: payload.Firstname,
            lastname: payload.Lastname,
            email: payload.Email,
            password: hashedPassword,
            phone: payload.Phone,
            companyName: payload.CompanyName,
            websiteName: payload.WebsiteName,
            websiteUrl: payload.WebsiteUrl);

        await _userRepository.Create(user);

        var token = new TokenDTO(
            _tokenService.Generate(),
            user.Id.ToString());

        await _authRepository.Set(token);

        return Result<AuthenticatedResponse>.Success(
            new AuthenticatedResponse(token));
    }

    public async Task<Result<AuthenticatedResponse>> LoginSupplier(LoginRequest payload)
    {
        var validation = _loginValidator.Validate(payload);
        if (!validation.IsValid)
            return Result<AuthenticatedResponse>.Failure(UserError.InvalidPayload);

        var user = await _userRepository.GetSupplierByEmail(payload.Email);
        if (user is null)
            return Result<AuthenticatedResponse>.Failure(UserError.InvalidCredentials);

        if (!_hashService.Verify(payload.Password, user.Password))
            return Result<AuthenticatedResponse>.Failure(UserError.InvalidCredentials);

        var token = new TokenDTO(
            _tokenService.Generate(),
            user.Id.ToString());

        await _authRepository.Set(token);

        return Result<AuthenticatedResponse>.Success(
            new AuthenticatedResponse(token));
    }

    public async Task<Result<AuthenticatedResponse>> RegisterCustomer(RegisterCustomerRequest payload)
    {
        var validation = _registerCustomerValidator.Validate(payload);
        if (!validation.IsValid)
            return Result<AuthenticatedResponse>.Failure(UserError.InvalidPayload);

        if (await _userRepository.GetCustomerByEmail(payload.Email) is not null)
            return Result<AuthenticatedResponse>.Failure(UserError.EmailAlreadyUsed);

        var hashedPassword = _hashService.Hash(payload.Password);
        if (string.IsNullOrEmpty(hashedPassword))
            return Result<AuthenticatedResponse>.Failure(UserError.InvalidPayload);

        var user = _mapper.Map<User>(payload);
        user.Password = hashedPassword;
        await _userRepository.Create(user);

        var token = new TokenDTO(
            _tokenService.Generate(),
            user.Id.ToString());

        await _authRepository.Set(token);

        return Result<AuthenticatedResponse>.Success(
            new AuthenticatedResponse(token));
    }

    public async Task<Result<AuthenticatedResponse>> LoginCustomer(LoginRequest payload)
    {
        var validation = _loginValidator.Validate(payload);
        if (!validation.IsValid)
            return Result<AuthenticatedResponse>.Failure(UserError.InvalidPayload);

        var user = await _userRepository.GetCustomerByEmail(payload.Email);
        if (user is null)
            return Result<AuthenticatedResponse>.Failure(UserError.InvalidCredentials);

        if (!_hashService.Verify(payload.Password, user.Password))
            return Result<AuthenticatedResponse>.Failure(UserError.InvalidCredentials);

        var token = new TokenDTO(
            _tokenService.Generate(),
            user.Id.ToString());

        await _authRepository.Set(token);

        return Result<AuthenticatedResponse>.Success(
            new AuthenticatedResponse(token));
    }

    public async Task Logout(string token)
    {
        if (await _authRepository.Get(token) is null)
            return;

        await _authRepository.Delete(token);
    }

    public async Task<Result<UserResponse>> ResetPassword(
        string userId,
        ResetPasswordRequest paylaod)
    {
        if (!paylaod.Validate())
            return Result<UserResponse>.Failure(UserError.InvalidPayload);

        if (!Guid.TryParse(userId, out var parsedId))
            return Result<UserResponse>.Failure(UserError.NotFound);

        var user = await _userRepository.GetCustomerById(parsedId);
        if (user is null)
            return Result<UserResponse>.Failure(UserError.NotFound);

        var hashedPassword = _hashService.Hash(paylaod.Password);
        if (string.IsNullOrEmpty(hashedPassword))
            return Result<UserResponse>.Failure(UserError.InvalidPayload);

        user.UpdateCustomer(password: hashedPassword);
        await _userRepository.Update(user);

        return Result<UserResponse>.Success(
            _mapper.Map<UserResponse>(user));
    }

    public async Task<User?> IsAuthenticated(string token)
    {
        if (string.IsNullOrEmpty(token)) return null;

        var userId = await _authRepository.Get(token);
        if (string.IsNullOrEmpty(userId)) return null;

        var user = await _userRepository.GetCustomerById(Guid.Parse(userId));

        return user;
    }
}
