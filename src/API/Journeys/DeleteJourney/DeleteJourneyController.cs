using API.Attributes;
using API.Extensions;
using Application.Journeys;
using Microsoft.AspNetCore.Mvc;

namespace API.Journeys.DeleteJourney;

[ApiController]
[Route("/journeys")]
public class DeleteJourneyController(IJourneyService service) : Controller
{
    private readonly IJourneyService _service = service;

    [Authenticated]
    [HttpDelete("{id}")]
    public async Task<IResult> Delete(string id)
    {
        var result = await _service.Delete(id);

        return result.IsSucess ?
            Results.NoContent() :
            ResultExtensions.FailureResult(result);
    }
}
