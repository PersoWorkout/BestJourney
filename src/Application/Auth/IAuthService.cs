using Domain.Abstractions;
using Domain.Auth.Requests;
using Domain.DTOs.Responses;
using Domain.Users;

namespace Application.Auth;

public interface IAuthService
{
    Task<Result<AuthenticatedResponse>> Register(CreateUserRequest payload);
    Task<Result<AuthenticatedResponse>> Login(LoginUserRequest payload);
    Task Logout(string token);
    Task<Result<UserResponse>> ResetPassword(
        string userId,
        ResetPasswordRequest payload);
    Task<Guid?> IsAuthenticated(string token);
}
