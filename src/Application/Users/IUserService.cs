using Domain.Abstractions;
using Domain.Users;
using Domain.Users.Requests;

namespace Application.Users;

public interface IUserService
{
    Task<Result<IEnumerable<UserResponse>>> GetUsers();
    Task<Result<UserResponse>> GetById(string id);
    Task<Result<UserResponse>> Update(string id, UpdateUserRequest payload);
    Task<Result<UserResponse>> Delete(string id);
}
