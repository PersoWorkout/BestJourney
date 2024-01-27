using Domain.Models;

namespace Application.Interfaces.Users
{
    public interface IUserRepository
    {
        bool CheckPassword(User user, string password);
        Task<User> Create(User user);
        Task<User?> GetById(string id);
        Task<User?> GetByEmail(string email);
        Task<User?> Update(User user);
        Task Delete(string id);
    }
}
