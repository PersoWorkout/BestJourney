using Application.Interfaces.Users;
using Domain.Models;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public bool CheckPassword(User user, string password)
        {
            throw new NotImplementedException();
        }

        public Task<User> Create(User user)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<User?> Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
