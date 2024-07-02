using Domain.Abstractions;
using Domain.Users;
using Domain.Users.Requests;

namespace Application.Interfaces.Users
{
    public interface IUserService
    {
        Task<Result<IEnumerable<UserResponse>>> GetUsers();
        Task<Result<UserResponse>> GetById(string id);
        Task<Result<UserResponse>> Update(string id, UpdateUserValidator payload);
        Task<Result<UserResponse>> Delete(string id);
    }
}
