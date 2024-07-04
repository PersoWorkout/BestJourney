using API.Attributes;
using API.Extensions;
using Application.Journeys;
using Domain.Journeys.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Journeys.UpdateJourney;

[ApiController]
[Route("/journeys")]
public class UpdtaeJourneyController(
    IJourneyService service, 
    UpdateJourneyPresenter presenter) : Controller
{
    private readonly IJourneyService _service = service;
    private readonly UpdateJourneyPresenter _presenter = presenter;

    [IsSupplier]
    [HttpPut("{id}")]
    public async Task<IResult> Handle(string id, [FromBody] UpdateJourneyRequest payload)
    {
        var result = await _service.Update(id, payload);

        return result.IsSucess ?
            Results.Ok(_presenter.ToJson(result.Data)) :
            ResultExtensions.FailureResult(result);
    }
}
