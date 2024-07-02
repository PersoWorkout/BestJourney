using AutoMapper;
using Domain.Journeys;
using Domain.Journeys.Requests;

namespace Application.Journeys;

public class JourneyProfile : Profile
{
    public JourneyProfile()
    {
        CreateMap<CreateJourneyRequest, Journey>();
        CreateMap<Journey, JourneyResponse>();
    }
}
