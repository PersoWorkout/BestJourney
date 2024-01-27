using Domain.Models;

namespace Application.Interfaces.Users
{
    public interface IUserRepository
    {
        bool CheckPassword(User user, string password);
        bool HashPassword(User user);
        Task<User> Create(User user);
        Task<User?> GetById(string id);
        Task<User?> GetByEmail(string email);
        Task<User?> Update(User user);
        Task Delete(User user);
    }
}
