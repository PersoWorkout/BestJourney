using Domain.Abstractions;
using Domain.DTOs.Validators.Orders;
using Domain.Models;

namespace Application.Interfaces.Orders;
public interface IOrderService
{
    Task<Result<IEnumerable<Order>>> GetByUser(string userId);
    Task<Result<IEnumerable<Order>>> GetByJourney(string journeyId);
    Task<Result<Order>> GetById(string orderId, string userId);
    Task<Result<Order>> Create(CreateOrderValidator payload, string userId);
    Task<Result<Order>> Update(string orderId, string userId, UpdateOrderValidator payload);
    Task<Result<object>> Delete(string orderId, string userId);
}
