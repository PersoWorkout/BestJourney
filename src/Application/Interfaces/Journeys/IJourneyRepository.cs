using Domain.Models;

namespace Application.Interfaces.Journeys
{
    public interface IJourneyRepository
    {
        Task<IEnumerable<Journey>> GetJourneys();
        Task Create(Journey journey);
        Task<Journey?> GetById(Guid id);
        Task SaveChanges(Journey journey);
        Task Delete(Journey journey);
    }
}
