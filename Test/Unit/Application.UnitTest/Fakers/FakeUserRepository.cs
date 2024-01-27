using Application.Interfaces.Users;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.Fakers
{
    public class FakeUserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();
        public async Task<User> Create(User user)
        {
            user.CreatedAt = DateTime.Now;
            _users.Add(user);
            return await Task.Run(() => user);
        }

        public async Task Delete(string id)
        {
            var userIndex = _users.FindIndex(u => u.Id.ToString() == id);
            await Task.Run(() => _users.RemoveAt(userIndex));
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
