using API.Attributes;
using API.Extensions;
using Application.Journeys;
using Domain.Journeys.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Journeys.CreateJourney;

[ApiController]
[Route("/journeys")]
public class CreateJourneyController(
    IJourneyService service, 
    CreateJourneyPresenter presenter) : Controller
{
    private readonly IJourneyService _service = service;
    private readonly CreateJourneyPresenter _presenter = presenter;

    [IsSupplier]
    [HttpPost]
    public async Task<IResult> Handle([FromBody] CreateJourneyRequest payload)
    {
        var result = await _service.Create(
            HttpContext.Items["userId"]!.ToString()!,
            payload);

        return result.IsSucess ?
            Results.Ok(_presenter.ToJson(result.Data)) :
            ResultExtensions.FailureResult(result);
    }
}
