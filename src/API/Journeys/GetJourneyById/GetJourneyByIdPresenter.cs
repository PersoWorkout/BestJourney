using Domain.Journeys;

namespace API.Journeys.GetJourneyById;

public class GetJourneyByIdPresenter
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
                isActive = journey.IsActive.ToString(),
                createdAt = journey.CreatedAt.ToString("dd/MM/yyyy HH:mm"),
                updatedAt = journey.UpdatedAt?.ToString("dd/MM/yyyy HH:mm"),
                creator = new
                {
                    id = journey.CreatorId.ToString(),
                    name = $"{journey.Creator.Firstname} {journey.Creator.Lastname}",
                    email = journey.Creator.Email,
                    companyName = journey.Creator.CompanyName,
                }
            }
        };
    }
}
