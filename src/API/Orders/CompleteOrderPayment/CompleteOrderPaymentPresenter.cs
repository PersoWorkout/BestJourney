using Domain.Orders;

namespace API.Orders.CompleteOrderPayment;

public class CompleteOrderPaymentPresenter
{
    public object ToJson(Order order)
    {
        return new
        {
            data = new
            {
                id = order.Id.ToString(),
                status = order.Status.ToString(),
                user = new
                {
                    id = order.UserId.ToString(),
                }
            }
        };
    }
}
