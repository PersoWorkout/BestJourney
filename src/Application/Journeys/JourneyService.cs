using Application.Users;
using AutoMapper;
using Domain.Abstractions;
using Domain.Journeys;
using Domain.Journeys.Requests;
using Domain.Users;
using FluentValidation;

namespace Application.Journeys;

public class JourneyService(
    IJourneyRepository journeyRepository,
    IUserRepository userRepository,
    IValidator<CreateJourneyRequest> validator,
    IMapper mapper) : IJourneyService
{
    private readonly IJourneyRepository _journeyRepository = journeyRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IValidator<CreateJourneyRequest> _validator = validator;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<IEnumerable<Journey>>> GetJourneys()
    {
        var journeys = await _journeyRepository.GetJourneys();

        return Result<IEnumerable<Journey>>.Success(journeys);
    }

    public async Task<Result<Journey>> Create(string userId, CreateJourneyRequest payload)
    {
        var validation = _validator.Validate(payload);
        if(!validation.IsValid)
            return Result<Journey>
                .Failure(JourneyError.InvalidPayload);

        if (!Guid.TryParse(userId, out var parsedUserId))
            return Result<Journey>.Failure(
                UserError.NotFound);

        var user = await _userRepository.GetSupplierById(parsedUserId);
        if (user is null)
            return Result<Journey>.Failure(
                UserError.NotFound);

        var journey = _mapper.Map<Journey>(payload);
        journey.CreatorId = user.Id;

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

    public async Task<Result<Journey>> Update(string id, string userId, UpdateJourneyRequest payload)
    {
        if (!Guid.TryParse(userId, out var parsedUserId))
            return Result<Journey>.Failure(
                UserError.NotFound);

        var user = await _userRepository.GetSupplierById(parsedUserId);
        if (user is null)
            return Result<Journey>.Failure(
                UserError.NotFound);

        if (!Guid.TryParse(id, out var journeyId))
            return Result<Journey>
                .Failure(JourneyError.NotFound(id));

        var journey = await _journeyRepository.GetById(journeyId);
        if (journey is null)
            return Result<Journey>
                .Failure(JourneyError.NotFound(id));

        if(journey.CreatorId != user.Id)
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

    public async Task<Result<object>> Delete(string id, string userId)
    {
        if (!Guid.TryParse(userId, out var parsedUserId))
            return Result<object>.Failure(
                UserError.NotFound);

        var user = await _userRepository.GetSupplierById(parsedUserId);
        if (user is null)
            return Result<object>.Failure(
                UserError.NotFound);

        if (!Guid.TryParse(id, out var journeyId))
            return Result<object>
                .Failure(JourneyError.NotFound(id));

        var journey = await _journeyRepository.GetById(journeyId);
        if (journey is null)
            return Result<object>
                .Failure(JourneyError.NotFound(id));

        if (journey.CreatorId != user.Id)
            return Result<object>
                .Failure(JourneyError.NotFound(id));

        await _journeyRepository.Delete(journey);

        return Result<object>.Success();
    }
}
