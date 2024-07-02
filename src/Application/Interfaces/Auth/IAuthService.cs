using Domain.Abstractions;
using Domain.Auth.Validators;
using Domain.DTOs;
using Domain.DTOs.Responses;
using Domain.Users;

namespace Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<Result<AuthenticatedResponse>> Register(CreateUserValidator payload);
        Task<Result<AuthenticatedResponse>> Login(LoginUserRequest payload);
        Task Logout(string token);
        Task<Result<UserResponse>> ResetPassword(
            string userId,
            ResetPasswordRequest payload);
        Task<Guid?> IsAuthenticated(string token);
    }
}
