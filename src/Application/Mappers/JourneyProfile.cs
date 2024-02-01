using AutoMapper;
using Domain.DTOs.Responses;
using Domain.DTOs.Validators.Journeys;
using Domain.Models;

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
