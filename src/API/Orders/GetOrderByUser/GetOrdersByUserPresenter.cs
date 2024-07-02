using Domain.Orders;

namespace API.Orders.GetOrderByUser;

public class GetOrdersByUserPresenter
{
    public object ToJson(IEnumerable<Order> orders)
    {
        return new
        {
            data = orders.Select(o => new
            {
                id = o.Id,
                userId = o.UserId,
                journeyId = o.JourneyId,
                price = o.Price,
                participentCount = o.ParticipentCount,
                status = o.Status.ToString(),
                createdAt = o.CreatedAt.ToString("dd/MM/yyyy HH:mm"),
            })
        };
    }
}
