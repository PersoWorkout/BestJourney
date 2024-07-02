using Domain.Utils;

namespace Domain.DTOs.Validators.Orders;
public class CreateOrderValidator
{
    public string JourneyId { get; set; }
    public int ParticipentCount { get; set; }

    public bool Validate()
    {
        return JourneyId.IsValid() &&
            ParticipentCount > 0;
    }
}
