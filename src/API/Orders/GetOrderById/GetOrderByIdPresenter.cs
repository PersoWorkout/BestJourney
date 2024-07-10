using Domain.Orders;

namespace API.Orders.GetOrderById;

public class GetOrderByIdPresenter
{
    public object ToJson(Order order)
    {
        return new
        {
            data = new
            {
                id = order.Id.ToString(),
                user = new
                {
                    id = order.User.Id.ToString(),
                    firstname = order.User.Firstname,
                    lastname = order.User.Lastname,
                    email = order.User.Email,
                },
                journey = new
                {
                    id = order.Journey.Id.ToString(),
                    name = order.Journey.Name,
                    description = order.Journey.Description,
                    price = order.Journey.Price.ToString("0.00€"),
                    creator = new
                    {
                        id = order.Journey.Creator.Id.ToString(),
                        email = order.Journey.Creator.Email
                    }
                },
                price = order.Price.ToString("0.00€"),
                status = order.Status.ToString(),
                createdAt = order.CreatedAt.ToString("dd/MM/yyyy hh:mm"),
            }
        };
    }
}
