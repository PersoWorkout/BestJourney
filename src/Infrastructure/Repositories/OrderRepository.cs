using Application.Orders;
using Domain.Orders;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderRepository(BestJourneyDbContext dbContext) : IOrderRepository
{
    private readonly BestJourneyDbContext _dbContext = dbContext;

    public async Task<Order> Create(Order order)
    {
        var data = await _dbContext.AddAsync(order);
        await _dbContext.SaveChangesAsync();

        return data.Entity;
    }

    public async Task Delete(Order order)
    {
        _dbContext.Remove(order);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Order?> GetById(Guid id)
    {
        return await _dbContext.Orders
            .Where(o => o.Id == id)
            .Include(o => o.Journey)
            .Include(o => o.User)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Order>> GetByJourney(Guid id)
    {
        return await _dbContext.Orders
            .Where(o => o.JourneyId == id)
            .Include(o => o.Journey)
            .Include(o => o.User)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetByUserId(Guid id)
    {
        return await _dbContext.Orders
            .Where(o => o.UserId == id)
            .Include(o => o.Journey)
            .Include(o => o.User)
            .ToListAsync();
    }

    public async Task<Order> Update(Order order)
    {
        var entity = _dbContext.Orders.Update(order).Entity;
        await _dbContext.SaveChangesAsync();

        return entity;
    }
}
