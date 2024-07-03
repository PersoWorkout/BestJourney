using AutoMapper;
using Domain.Abstractions;
using Domain.Journeys;
using Domain.Journeys.Requests;

namespace Application.Journeys;

public class JourneyService(
    IJourneyRepository journeyRepository,
    IMapper mapper) : IJourneyService
{
    private readonly IJourneyRepository _journeyRepository = journeyRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<IEnumerable<Journey>>> GetJourneys()
    {
        var journeys = await _journeyRepository.GetJourneys();

        return Result<IEnumerable<Journey>>.Success(journeys);
    }

    public async Task<Result<Journey>> Create(CreateJourneyRequest payload)
    {
        if (!payload.Validate())
            return Result<Journey>
                .Failure(JourneyError.InvalidPayload);

        var journey = _mapper.Map<Journey>(payload);

        await _journeyRepository.Create(journey);

        return Result<Journey>.Success(journey);
    }
    public async Task<Result<Journey>> GetById(string id)
    {
        if (!Guid.TryParse(id, out var journeyId))
            return Result<Journey>
                .Failure(JourneyError.NotFound(id));

        var journey = await _journeyRepository.GetById(journeyId);
        if (journey == null)
            return Result<Journey>
                .Failure(JourneyError.NotFound(id));

        return Result<Journey>.Success(journey);
    }

    public async Task<Result<Journey>> Update(string id, UpdateJourneyRequest payload)
    {
        if (!payload.Validate())
            return Result<Journey>
                .Failure(JourneyError.InvalidPayload);

        if (!Guid.TryParse(id, out var journeyId))
            return Result<Journey>
                .Failure(JourneyError.NotFound(id));

        var journey = await _journeyRepository.GetById(journeyId);
        if (journey is null)
            return Result<Journey>
                .Failure(JourneyError.NotFound(id));

        journey.Update(
            payload.Name,
            payload.Description,
            payload.Country,
            payload.City,
            payload.Price,
            payload.IsActive);

        await _journeyRepository.SaveChanges(journey);

        return Result<Journey>.Success(journey);

    }

    public async Task<Result<object>> Delete(string id)
    {
        if (!Guid.TryParse(id, out var journeyId))
            return Result<object>
                .Failure(JourneyError.NotFound(id));

        var journey = await _journeyRepository.GetById(journeyId);
        if (journey is null)
            return Result<object>
                .Failure(JourneyError.NotFound(id));

        await _journeyRepository.Delete(journey);

        return Result<object>.Success();
    }
}
