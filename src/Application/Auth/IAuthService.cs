using Domain.Abstractions;
using Domain.Auth.Requests;
using Domain.Auth.Requests.Customers;
using Domain.Auth.Requests.Suppliers;
using Domain.DTOs.Responses;
using Domain.Users;

namespace Application.Auth;

public interface IAuthService
{
    Task<Result<AuthenticatedResponse>> RegisterSupplier(RegisterSupplierRequest payload);
    Task<Result<AuthenticatedResponse>> LoginSupplier(LoginRequest payload);
    Task<Result<AuthenticatedResponse>> RegisterCustomer(RegisterCustomerRequest payload);
    Task<Result<AuthenticatedResponse>> LoginCustomer(LoginRequest payload);
    Task Logout(string token);
    Task<Result<UserResponse>> ResetPassword(
        string userId,
        ResetPasswordRequest payload);
    Task<User?> IsAuthenticated(string token);
}
