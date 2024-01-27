using Application.Interfaces.Users;
using Domain.Models;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository(BestJourneyDataContext context) : IUserRepository
    {
        private readonly BestJourneyDataContext _context = context;

        public bool CheckPassword(User user, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }

        public async Task<User> Create(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
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
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            throw new NotImplementedException();
        }
    }
}
