using Application.Interfaces.Orders;
using Domain.Orders;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OrderRepository(BestJourneyDbContext dbContext) : IOrderRepository
    {
        private readonly BestJourneyDbContext _dbContext = dbContext;

        public async Task<Order> Create(Order order)
        {
            var data = await _dbContext.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            return data.Entity;
        }

        public Task Delete(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetByJourney(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetByUserId(Guid id)
        {
            return await _dbContext.Orders
                .Where(o => o.UserId == id)
                .Include(o => o.Journey)
                .ToListAsync();
        }

        public Task<Order> Update(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
