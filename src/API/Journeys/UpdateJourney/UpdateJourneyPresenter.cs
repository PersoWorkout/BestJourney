using Domain.Journeys;

namespace API.Journeys.UpdateJourney;

public class UpdateJourneyPresenter
{
    public object ToJson(Journey journey)
    {
        return new
        {
            data = new
            {
                id = journey.Id.ToString(),
                name = journey.Name,
                description = journey.Description,
                price = journey.Price.ToString("0.00€"),
                isActive = journey.IsActive.ToString(),
                createdAt = journey.CreatedAt.ToString("dd/MM/yyyy hh:mm"),
                updatedAt = journey.UpdatedAt?.ToString("dd/MM/yyyy hh:mm")
            }
        };
    }
}
