using Domain.Abstractions;
using Domain.DTOs;
using Domain.DTOs.Responses;
using Domain.DTOs.Validators.Users;

namespace Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<Result<AuthenticatedResponse>> Register(CreateUserValidator payload);
        Task<Result<AuthenticatedResponse>> Login(LoginUserValidator payload);
        Task Logout(string token);
        Task<Result<UserResponse>> ResetPassword(
            string userId,
            ResetPasswordValidator payload);
        Task<Guid?> IsAuthenticated(string token);
    }
}
