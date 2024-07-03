using API.Extensions;
using Application.Journeys;
using Microsoft.AspNetCore.Mvc;

namespace API.Journeys.GetJourneyById;

[ApiController]
[Route("/journeys")]
public class GetJourneyByIdController(
    IJourneyService service,
    GetJourneyByIdPresenter presenter) : Controller
{
    private readonly IJourneyService _service = service;
    private readonly GetJourneyByIdPresenter _presenter = presenter;

    [HttpGet("{id}")]
    public async Task<IResult> GetById(string id)
    {
        var result = await _service.GetById(id);

        return result.IsSucess ?
            Results.Ok(_presenter.ToJson(result.Data)) :
            ResultExtensions.FailureResult(result);
    }
}
