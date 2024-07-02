using Domain.Orders;

namespace Application.Interfaces.Orders;
public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetByUserId(Guid id);
    Task<IEnumerable<Order>> GetByJourney(Guid id);
    Task<Order> GetById(Guid id);
    Task<Order> Create(Order order);
    Task<Order> Update(Order order);
    Task Delete(Order order);
}

