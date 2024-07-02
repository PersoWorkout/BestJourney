using Domain.Utils;

namespace Domain.Orders.Requests;
public class CreateOrderRequest
{
    public string JourneyId { get; set; }
    public int ParticipentCount { get; set; }

    public bool Validate()
    {
        return JourneyId.IsValid() &&
            ParticipentCount > 0;
    }
}
