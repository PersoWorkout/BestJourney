using Domain.Abstractions;
using Domain.Auth.Requests;
using Domain.DTOs.Responses;
using Domain.Users;

namespace Application.Auth;

public interface IAuthService
{
    Task<Result<AuthenticatedResponse>> RegisterSupplier(RegisterSupplierRequest payload);
    Task<Result<AuthenticatedResponse>> RegisterCustomer(CreateUserRequest payload);
    Task<Result<AuthenticatedResponse>> LoginCustomer(LoginUserRequest payload);
    Task<Result<AuthenticatedResponse>> LoginSupplier(LoginUserRequest payload);
    Task Logout(string token);
    Task<Result<UserResponse>> ResetPassword(
        string userId,
        ResetPasswordRequest payload);
    Task<Guid?> IsAuthenticated(string token);
}
