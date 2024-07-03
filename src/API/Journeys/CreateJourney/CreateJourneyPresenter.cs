using Domain.Journeys;

namespace API.Journeys.CreateJourney;

public class CreateJourneyPresenter
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
                country = journey.Country,
                city = journey.City,
            }
        };
    }
}
