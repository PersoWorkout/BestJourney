using Domain.Orders;

namespace API.Orders.CreateOrder
{
    public class CreateOrderPresenter
    {
        public object ToJson(Order order)
        {
            return new
            {
                data = new
                {
                    id = order.Id,
                    userId = order.UserId,
                    journeyId = order.JourneyId,
                    price = order.Price,
                    participentCount = order.ParticipentCount,
                    status = order.Status.ToString(),
                    createdAt = order.CreatedAt.ToString("dd/MM/yyyy HH:mm"),
                    journey = new
                    {
                        id = order.JourneyId,
                        name = order.Journey.Name,
                        location = $"{order.Journey.Country}/{order.Journey.City}",
                    }
                }
            };
        }
    }
}
