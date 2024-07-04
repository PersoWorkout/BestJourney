using Application.Journeys;
using Domain.Journeys;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class JourneyRepository(
    BestJourneyDbContext dbContext) : IJourneyRepository
{
    private readonly BestJourneyDbContext _dbContext = dbContext;

    public async Task<IEnumerable<Journey>> GetJourneys()
    {
        return await _dbContext.Journeys.ToListAsync();
    }

    public async Task Create(Journey journey)
    {
        await _dbContext.Journeys.AddAsync(journey);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Journey?> GetById(Guid id)
    {
        return await _dbContext.Journeys
            .Where(x => x.Id == id)
            .Include(x => x.Creator)
            .FirstOrDefaultAsync();
    }

    public async Task SaveChanges(Journey journey)
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Journey journey)
    {
        _dbContext.Journeys.Remove(journey);
        await _dbContext.SaveChangesAsync();
    }
}
