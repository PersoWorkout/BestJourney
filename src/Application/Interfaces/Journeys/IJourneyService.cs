using AutoMapper;
using Domain.Abstractions;
using Domain.DTOs.Responses;
using Domain.DTOs.Validators.Journeys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Journeys
{
    public interface IJourneyService
    {
        Task<Result<IEnumerable<JourneyResponse>>> GetJourneys();
        Task<Result<JourneyResponse>> Create(CreateJourneyValidator payload);
        Task<Result<JourneyResponse>> GetById(string id);
        Task<Result<JourneyResponse>> Update(string id, UpdateJourneyValidator payload);
        Task<Result<JourneyResponse>> Delete(string id);
    }
}
