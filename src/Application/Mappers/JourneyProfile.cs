using AutoMapper;
using Domain.DTOs.Responses;
using Domain.DTOs.Validators.Journeys;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
