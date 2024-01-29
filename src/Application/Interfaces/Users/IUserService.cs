using Domain.Abstractions;
using Domain.DTOs.Responses;
using Domain.DTOs.Validators.Users;

namespace Application.Interfaces.Users
{
    public interface IUserService
    {
        Task<Result<IEnumerable<UserResponse>>> GetUsers();
        Task<Result<UserResponse>> GetById(string id);
        Task<Result<string>> Login(LoginUserValidator payload);
        Task<Result<UserResponse>> Create(CreateUserValidator payload);
        Task<Result<UserResponse>> Update(string id, UpdateUserValidator payload);
        Task<Result<UserResponse>> Delete(string id);
    }
}
