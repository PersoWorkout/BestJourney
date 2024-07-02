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

    public async Task<Result<IEnumerable<JourneyResponse>>> GetJourneys()
    {
        var journeys = await _journeyRepository.GetJourneys();

        return Result<IEnumerable<JourneyResponse>>
            .Success(journeys.Select(journey => _mapper.Map<JourneyResponse>(journey)).ToList());
    }

    public async Task<Result<JourneyResponse>> Create(CreateJourneyRequest payload)
    {
        if (!payload.Validate())
            return Result<JourneyResponse>
                .Failure(JourneyError.InvalidPayload);

        var journey = _mapper.Map<Journey>(payload);

        await _journeyRepository.Create(journey);

        return Result<JourneyResponse>
            .Success(_mapper.Map<JourneyResponse>(journey));
    }
    public async Task<Result<JourneyResponse>> GetById(string id)
    {
        if (!Guid.TryParse(id, out var journeyId))
            return Result<JourneyResponse>
                .Failure(JourneyError.NotFound(id));

        var journey = await _journeyRepository.GetById(journeyId);
        if (journey == null)
            return Result<JourneyResponse>
                .Failure(JourneyError.NotFound(id));

        return Result<JourneyResponse>.Success(
            _mapper.Map<JourneyResponse>(journey));
    }

    public async Task<Result<JourneyResponse>> Update(string id, UpdateJourneyRequest payload)
    {
        if (!payload.Validate())
            return Result<JourneyResponse>
                .Failure(JourneyError.InvalidPayload);

        if (!Guid.TryParse(id, out var journeyId))
            return Result<JourneyResponse>
                .Failure(JourneyError.NotFound(id));

        var journey = await _journeyRepository.GetById(journeyId);
        if (journey is null)
            return Result<JourneyResponse>
                .Failure(JourneyError.NotFound(id));

        journey.Update(
            payload.Name,
            payload.Description,
            payload.Country,
            payload.City,
            payload.Price,
            payload.IsActive);

        await _journeyRepository.SaveChanges(journey);

        return Result<JourneyResponse>.Success(
            _mapper.Map<JourneyResponse>(journey));

    }

    public async Task<Result<JourneyResponse>> Delete(string id)
    {
        if (!Guid.TryParse(id, out var journeyId))
            return Result<JourneyResponse>
                .Failure(JourneyError.NotFound(id));

        var journey = await _journeyRepository.GetById(journeyId);
        if (journey is null)
            return Result<JourneyResponse>
                .Failure(JourneyError.NotFound(id));

        await _journeyRepository.Delete(journey);

        return Result<JourneyResponse>.Success();
    }
}
