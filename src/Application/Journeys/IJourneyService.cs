using Domain.Abstractions;
using Domain.Journeys;
using Domain.Journeys.Requests;

namespace Application.Journeys;

public interface IJourneyService
{
    Task<Result<IEnumerable<Journey>>> GetJourneys();
    Task<Result<Journey>> Create(string userId, CreateJourneyRequest payload);
    Task<Result<Journey>> GetById(string id);
    Task<Result<Journey>> Update(string id, string userId, UpdateJourneyRequest payload);
    Task<Result<object>> Delete(string id, string userId);
}
