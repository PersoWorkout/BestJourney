using Domain.Abstractions;
using Domain.Journeys;
using Domain.Journeys.Requests;

namespace Application.Journeys;

public interface IJourneyService
{
    Task<Result<IEnumerable<JourneyResponse>>> GetJourneys();
    Task<Result<JourneyResponse>> Create(CreateJourneyRequest payload);
    Task<Result<JourneyResponse>> GetById(string id);
    Task<Result<JourneyResponse>> Update(string id, UpdateJourneyRequest payload);
    Task<Result<JourneyResponse>> Delete(string id);
}
