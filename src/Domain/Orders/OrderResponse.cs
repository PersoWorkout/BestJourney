using Domain.Journeys;

namespace Domain.Orders;
public class OrderResponse
{
    public required Journey Journey { get; set; }
    public required int ParticipantCount { get; set; }
    public required decimal Price { get; set; }
    public required string Status { get; set; }
}
