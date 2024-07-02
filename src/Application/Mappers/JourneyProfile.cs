using AutoMapper;
using Domain.Journeys;
using Domain.Journeys.Validators;

namespace Application.Mappers
{
    public class JourneyProfile: Profile
    {
        public JourneyProfile()
        {
            CreateMap<CreateJourneyValidator, Journey>();
            CreateMap<Journey, JourneyResponse>();
        }
    }
}
