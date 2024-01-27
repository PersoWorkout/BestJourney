using Application.Interfaces.Users;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository(BestJourneyDbContext dbContext) : IUserRepository
    {
        private readonly BestJourneyDbContext _dbContext = dbContext;

        public bool CheckPassword(User user, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }

        public void HashPassword(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        }

        public async Task<User> Create(User user)
        {
            var result =await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task Delete(User user)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _dbContext
                .Users
                .FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<User?> GetById(string id)
        {
            return await _dbContext
                .Users
                .FirstOrDefaultAsync(user => user.Id.ToString() == id);
        }

        public async Task<User?> Update(User user)
        {
            await _dbContext.SaveChangesAsync();
            return user;
        }
    }
}
