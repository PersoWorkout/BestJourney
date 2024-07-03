using Domain.Orders;

namespace API.Orders.UpdateOrder;

public class UpdateOrderPresenter
{
    public object ToJson(Order order)
    {
        return new
        {
            data = new
            {
                id = order.Id.ToString(),
                userId = order.UserId.ToString(),
                journeyId = order.Journey.Id.ToString(),
                price = order.Price.ToString("0.00€"),
                status = order.Status.ToString(),
            }
        };
    }
}
