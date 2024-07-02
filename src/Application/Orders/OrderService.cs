using Application.Journeys;
using Application.Users;
using AutoMapper;
using Domain.Abstractions;
using Domain.Journeys;
using Domain.Orders;
using Domain.Orders.Requests;
using Domain.Users;

namespace Application.Orders;

public class OrderService(
    IOrderRepository repository,
    IUserRepository userRepository,
    IJourneyRepository journeyReposirtory,
    IMapper mapper) : IOrderService
{
    private readonly IOrderRepository _repository = repository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IJourneyRepository _journeyReposirtory = journeyReposirtory;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<IEnumerable<Order>>> GetByUser(string userId)
    {
        if (!Guid.TryParse(userId, out var parsedUserId))
            return Result<IEnumerable<Order>>
                .Failure(UserError.NotFound);

        var user = await _userRepository.GetById(parsedUserId);
        if (user is null)
            return Result<IEnumerable<Order>>
                .Failure(UserError.NotFound);

        var orders = await _repository.GetByUserId(user.Id);

        return Result<IEnumerable<Order>>.Success(orders);
    }
    public async Task<Result<Order>> Create(CreateOrderRequest payload, string userId)
    {
        if (!Guid.TryParse(userId, out var parsedUserId))
            return Result<Order>
                .Failure(UserError.NotFound);

        var user = await _userRepository.GetById(parsedUserId);
        if (user is null)
            return Result<Order>
                .Failure(UserError.NotFound);

        if (!payload.Validate())
            return Result<Order>
                .Failure(OrderError.InvalidPayload);

        if (!Guid.TryParse(payload.JourneyId, out var parsedJourneyId))
            return Result<Order>
                .Failure(JourneyError.NotFound(payload.JourneyId));

        var journey = await _journeyReposirtory.GetById(parsedJourneyId);
        if (journey is null)
            return Result<Order>
                .Failure(JourneyError.NotFound(payload.JourneyId));

        var order = _mapper.Map<Order>(payload);
        order.UserId = parsedUserId;
        order.Price = journey.Price * payload.ParticipentCount;

        var result = await _repository.Create(order);

        return Result<Order>.Success(result);
    }

    public Task<Result<object>> Delete(string orderId, string userId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Order>> GetById(string orderId, string userId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<Order>>> GetByJourney(string journeyId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Order>> Update(string orderId, string userId, UpdateOrderRequest payload)
    {
        throw new NotImplementedException();
    }
}
