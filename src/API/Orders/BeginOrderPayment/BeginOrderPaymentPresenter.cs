using Domain.Orders;

namespace API.Orders.BeginOrderPayment;

public class BeginOrderPaymentPresenter
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
