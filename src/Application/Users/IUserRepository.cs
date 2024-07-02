using Domain.Users;

namespace Application.Users;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetUsers();
    Task<User> Create(User user);
    Task<User?> GetById(Guid id);
    Task<User?> GetByEmail(string email);
    Task<User?> Update(User user);
    Task Delete(User user);
}
