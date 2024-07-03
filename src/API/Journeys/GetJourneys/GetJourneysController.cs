using Application.Journeys;
using Microsoft.AspNetCore.Mvc;

namespace API.Journeys.GetJourneys;

[ApiController]
[Route("/journeys")]
public class GetJourneysController(
    IJourneyService service,
    GetJourneysPresenter presenter) : Controller
{
    private readonly IJourneyService _service = service;
    private readonly GetJourneysPresenter _presenter = presenter;

    [HttpGet]
    public async Task<IResult> Index()
    {
        var result = await _service.GetJourneys();
        return Results.Ok(_presenter.ToJson(result.Data));
    }
}
