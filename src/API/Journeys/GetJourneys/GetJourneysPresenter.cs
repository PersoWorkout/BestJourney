using Domain.Journeys;

namespace API.Journeys.GetJourneys;

public class GetJourneysPresenter
{
    public object ToJson(IEnumerable<Journey> journeys)
    {
        return new
        {
            data = journeys.Select(j => new
            {
                id = j.Id.ToString(),
                name = j.Name,
                description = j.Description,
                price = j.Price.ToString("0.00€")
            }).ToList()
        };
    }
}
