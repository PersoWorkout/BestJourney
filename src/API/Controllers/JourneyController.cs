using Application.Interfaces.Journeys;
using Domain.DTOs.Validators.Journeys;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/journeys")]
    public class JourneyController(IJourneyService journeyService) : 
        Controller
    {
        private readonly IJourneyService _journeyService = journeyService;

        [HttpGet]
        public async Task<IResult> Index()
        {
            var journeys = await _journeyService.GetJourneys();
            return Results.Ok(journeys.Data);
        }

        [HttpPost]
        public async Task<IResult> Create([FromBody] CreateJourneyValidator payload)
        {
            var journey = await _journeyService.Create(payload);
            return journey.IsSucess ?
                Results.Created() :
                Results.Problem(
                    statusCode: StatusCodes.Status400BadRequest,
                    title: "Bad Request",
                    extensions: new Dictionary<string, object?>
                    {
                        {"errors", new [] {journey.Error } }
                    });
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetById(string id)
        {
            var journey = await _journeyService.GetById(id);
            return journey.IsSucess ?
                Results.Ok(journey.Data) :
                Results.Problem(
                    statusCode: StatusCodes.Status400BadRequest,
                    title: "Bad Request",
                    extensions: new Dictionary<string, object?>
                    {
                        {"errors", new [] {journey.Error } }
                    });
        }

        [HttpPut("{id}")]
        public async Task<IResult> Update(string id, [FromBody] UpdateJourneyValidator payload)
        {
            var journey = await _journeyService.Update(id, payload);
            return journey.IsSucess ?
                Results.Ok(journey.Data) :
                Results.Problem(
                    statusCode: StatusCodes.Status400BadRequest,
                    title: "Bad Request",
                    extensions: new Dictionary<string, object?>
                    {
                        {"errors", new [] {journey.Error } }
                    });
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Delete(string id)
        {
            var result = await _journeyService.Delete(id);
            return result.IsSucess ?
                Results.NoContent() :
                Results.Problem(
                    statusCode: StatusCodes.Status400BadRequest,
                    title: "Bad Request",
                    extensions: new Dictionary<string, object?>
                    {
                        {"errors", new [] {result.Error } }
                    });
        }
    }
}
