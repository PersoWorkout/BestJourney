using Application.Users;
using Domain.Users;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(BestJourneyDbContext dbContext) : IUserRepository
{
    private readonly BestJourneyDbContext _dbContext = dbContext;

    public async Task<IEnumerable<User>> GetCustomers()
    {
        return await _dbContext.Users
            .Where(x => x.Role == UserRole.Customer)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetSuppliers()
    {
        return await _dbContext.Users
            .Where(x => x.Role == UserRole.Supplier)
            .ToListAsync();
    }


    public async Task<User> Create(User user)
    {
        var result =await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<User?> GetCustomerById(Guid id)
    {
        return await _dbContext.Users
            .Where(x => x.Id == id && x.Role == UserRole.Customer)
            .FirstOrDefaultAsync();
    }

    public async Task<User?> GetSupplierById(Guid id)
    {
        return await _dbContext.Users
            .Where(x => x.Id == id && x.Role == UserRole.Supplier)
            .FirstOrDefaultAsync();
    }

    public async Task<User?> GetCustomerByEmail(string email)
    {
        return await _dbContext.Users
            .Where(x => x.Email == email && x.Role == UserRole.Customer)
            .FirstOrDefaultAsync();
    }

    public async Task<User?> GetSupplierByEmail(string email)
    {
        return await _dbContext.Users
            .Where(x => x.Email == email && x.Role == UserRole.Supplier)
            .FirstOrDefaultAsync();
    }

    public async Task<UserRole> GetRole(Guid id)
    {
        return await _dbContext.Users
            .Where(x => x.Id == id)
            .Select(x => x.Role)
            .FirstOrDefaultAsync();
    }

    public async Task<User?> Update(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task Delete(User user)
    {
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
    }
}
