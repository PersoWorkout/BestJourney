using Application.Interfaces.Users;
using Domain.Models;

namespace Application.UnitTest.Fakers
{
    public class FakeUserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await Task.Run(() => _users);
        }

        public async Task<User> Create(User user)
        {
            user.CreatedAt = DateTime.Now;
            _users.Add(user);
            return await Task.Run(() => user);
        }

        public async Task<User> GetById(Guid id)
        {
            return await Task.Run(() => 
            _users
            .FirstOrDefault(user => user.Id == id));
        }

        public async Task Delete(User user)
        {
            await Task.Run(() => _users.Remove(user));
        }

        public async Task<User?> GetById(string id)
        {
            return await Task.Run(
                () => _users.FirstOrDefault(u => u.Id.ToString() == id));
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await Task.Run(
                () => _users.FirstOrDefault(u => u.Email == email));
        }

        public bool CheckPassword(User user, string password)
        {
            return user.Password == password;
        }

        public bool HashPassword(User user)
        {
            return true;
        }

        public async Task<User?> Update(User user)
        {
            var userIndex = _users.FindIndex(u => u.Id == user.Id);

            if (userIndex == -1) return null;

            user.UpdatedAt = DateTime.Now;
            _users[userIndex] = user;

            return await Task.Run(() => user);
        }
    }
}
