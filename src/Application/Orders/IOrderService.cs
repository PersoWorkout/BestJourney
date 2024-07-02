using Domain.Abstractions;
using Domain.Orders;
using Domain.Orders.Requests;

namespace Application.Orders;
public interface IOrderService
{
    Task<Result<IEnumerable<Order>>> GetByUser(string userId);
    Task<Result<IEnumerable<Order>>> GetByJourney(string journeyId);
    Task<Result<Order>> GetById(string orderId, string userId);
    Task<Result<Order>> Create(CreateOrderRequest payload, string userId);
    Task<Result<Order>> Update(string orderId, string userId, UpdateOrderRequest payload);
    Task<Result<object>> Delete(string orderId, string userId);
}
