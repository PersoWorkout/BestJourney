namespace Domain.Journeys.Requests;

public class CreateJourneyRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; } = false;
}
