using Application.Interfaces.Users;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository(BestJourneyDbContext dbContext) : IUserRepository
    {
        private readonly BestJourneyDbContext _dbContext = dbContext;

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _dbContext.Users.ToListAsync();
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

        public async Task<User?> GetById(Guid id)
        {
            return await _dbContext
                .Users
                .FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<User?> Update(User user)
        {
            await _dbContext.SaveChangesAsync();
            return user;
        }
    }
}
