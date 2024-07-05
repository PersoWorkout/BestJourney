using Application.Journeys;
using Application.Users;
using AutoMapper;
using Domain.Abstractions;
using Domain.Journeys;
using Domain.Orders;
using Domain.Orders.Requests;
using Domain.Users;
using FluentValidation;

namespace Application.Orders;

public class OrderService(
    IOrderRepository repository,
    IUserRepository userRepository,
    IJourneyRepository journeyReposirtory,
    IValidator<CreateOrderRequest> validator,
    IMapper mapper) : IOrderService
{
    private readonly IOrderRepository _repository = repository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IJourneyRepository _journeyReposirtory = journeyReposirtory;
    private readonly IValidator<CreateOrderRequest> _validator = validator;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<IEnumerable<Order>>> GetByCustomer(string customerId)
    {
        if (!Guid.TryParse(customerId, out var parsedUserId))
            return Result<IEnumerable<Order>>
                .Failure(UserError.NotFound);

        var user = await _userRepository.GetCustomerById(parsedUserId);
        if (user is null)
            return Result<IEnumerable<Order>>
                .Failure(UserError.NotFound);

        var orders = await _repository.GetByUserId(user.Id);

        return Result<IEnumerable<Order>>.Success(orders);
    }
    public async Task<Result<Order>> Create(string customerId, CreateOrderRequest payload)
    {
        var validation = _validator.Validate(payload);
        if(!validation.IsValid)
            return Result<Order>
                .Failure(OrderError.InvalidPayload);

        if (!Guid.TryParse(customerId, out var parsedUserId))
            return Result<Order>
                .Failure(UserError.NotFound);

        var user = await _userRepository.GetCustomerById(parsedUserId);
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

    public async Task<Result<object>> Delete(string orderId, string customerId)
    {
        if (!Guid.TryParse(customerId, out var parsedUserId))
            return Result<object>.Failure(OrderError.NotFound(orderId));

        if (!Guid.TryParse(orderId, out var parsedOrderId))
            return Result<object>.Failure(OrderError.NotFound(orderId));

        var order = await _repository.GetById(parsedOrderId);
        if (order is null)
            return Result<object>.Failure(OrderError.NotFound(orderId));

        if(order.UserId != parsedUserId)
            return Result<object>.Failure(OrderError.NotFound(orderId));

        await _repository.Delete(order);

        return Result<object>.Success();
    }

    public async Task<Result<Order>> GetById(string orderId)
    {
        if(!Guid.TryParse(orderId, out var parsedOrderId))
            return Result<Order>.Failure(OrderError.NotFound(orderId));

        var order = await _repository.GetById(parsedOrderId);
        if(order is null)
            return Result<Order>.Failure(OrderError.NotFound(orderId));

        return Result<Order>.Success(order);
    }

    public async Task<Result<IEnumerable<Order>>> GetByJourney(string journeyId)
    {
        if(!Guid.TryParse(journeyId, out var parsedJourneyId))
            return Result<IEnumerable<Order>>.Failure(
                JourneyError.NotFound(journeyId));

        var journey = await _journeyReposirtory.GetById(parsedJourneyId);
        if(journey is null)
            return Result<IEnumerable<Order>>.Failure(
                JourneyError.NotFound(journeyId));

        var orders = await _repository.GetByJourney(parsedJourneyId);
        return Result<IEnumerable<Order>>.Success(orders);
    }

    public async Task<Result<Order>> Update(string orderId, string customerId, UpdateOrderRequest payload)
    {
        if (!Guid.TryParse(customerId, out var parsedUserId))
            return Result<Order>.Failure(OrderError.NotFound(orderId));

        if (!Guid.TryParse(orderId, out var parsedOrderId))
            return Result<Order>.Failure(OrderError.NotFound(orderId));

        var order = await _repository.GetById(parsedOrderId);
        if (order is null)
            return Result<Order>.Failure(OrderError.NotFound(orderId));

        if (order.UserId != parsedUserId)
            return Result<Order>.Failure(OrderError.NotFound(orderId));

        order.Update(payload.ParticipantCount);

        var result = await _repository.Update(order);

        return Result<Order>.Success(result);
    }
}
