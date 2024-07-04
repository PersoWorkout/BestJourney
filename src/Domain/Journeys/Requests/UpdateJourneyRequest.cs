using Domain.Utils;

namespace Domain.Journeys.Requests;

public class UpdateJourneyRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public decimal Price { get; set; } = 0m;
    public bool IsActive { get; set; } = false;
}
