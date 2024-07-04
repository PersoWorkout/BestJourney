using Domain.Abstractions;
using Domain.Orders;
using Domain.Orders.Requests;

namespace Application.Orders;
public interface IOrderService
{
    Task<Result<IEnumerable<Order>>> GetByCustomer(string customerId);
    Task<Result<IEnumerable<Order>>> GetByJourney(string journeyId);
    Task<Result<Order>> GetById(string orderId);
    Task<Result<Order>> Create(string customerId, CreateOrderRequest payload);
    Task<Result<Order>> Update(string orderId, string customerId, UpdateOrderRequest payload);
    Task<Result<object>> Delete(string orderId, string customerId);
}
