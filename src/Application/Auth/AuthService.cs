using Application.Users;
using AutoMapper;
using Domain.Abstractions;
using Domain.Auth;
using Domain.Auth.Requests;
using Domain.DTOs.Responses;
using Domain.Users;

namespace Application.Auth;

public class AuthService(
    IAuthRepository authRepository,
    IHashService hashService,
    IMapper mapper,
    ITokenService tokenService,
    IUserRepository userRepository) : IAuthService
{
    private readonly IAuthRepository _authRepository = authRepository;
    private readonly IHashService _hashService = hashService;
    private readonly IMapper _mapper = mapper;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result<AuthenticatedResponse>> Register(CreateUserRequest payload)
    {
        if (!payload.Validate())
            return Result<AuthenticatedResponse>.Failure(UserError.InvalidPayload);

        if (await _userRepository.GetByEmail(payload.Email) is not null)
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

    public async Task<Result<AuthenticatedResponse>> Login(LoginUserRequest payload)
    {
        if (!payload.Validate())
            return Result<AuthenticatedResponse>.Failure(UserError.InvalidPayload);

        var user = await _userRepository.GetByEmail(payload.Email);
        if (user is null)
            return Result<AuthenticatedResponse>.Failure(UserError.InvalidCredentials);

        if (!_hashService.Verify(payload.Password, user.Password))
            return Result<AuthenticatedResponse>.Failure(UserError.InvalidCredentials);

        var token = new TokenDTO(
            _tokenService.Generate(),
            user.Id.ToString());

        await _userRepository.Update(user);

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

        var user = await _userRepository.GetById(parsedId);
        if (user is null)
            return Result<UserResponse>.Failure(UserError.NotFound);

        var hashedPassword = _hashService.Hash(paylaod.Password);
        if (string.IsNullOrEmpty(hashedPassword))
            return Result<UserResponse>.Failure(UserError.InvalidPayload);

        user.Update(password: hashedPassword);
        await _userRepository.Update(user);

        return Result<UserResponse>.Success(
            _mapper.Map<UserResponse>(user));
    }

    public async Task<Guid?> IsAuthenticated(string token)
    {
        if (string.IsNullOrEmpty(token)) return null;

        var userId = await _authRepository.Get(token);
        if (string.IsNullOrEmpty(userId)) return null;

        return Guid.Parse(userId);
    }
}
