using API.Attributes;
using API.Extensions;
using Application.Orders;
using Microsoft.AspNetCore.Mvc;

namespace API.Orders.GetOrderById;

[ApiController]
[Route("/orders")]
public class GetOrderByIdController(
    IOrderService service,
    GetOrderByIdPresenter presenter) : Controller
{
    private readonly IOrderService _service = service;
    private readonly GetOrderByIdPresenter _presenter = presenter;

    [Authenticated]
    [HttpGet("{id}")]
    public async Task<IResult> Handle(string id)
    {
        var result = await _service.GetById(id);

        return result.IsSucess ?
            Results.Ok(_presenter.ToJson(result.Data!)) :
            ResultExtensions.FailureResult(result);
    }
}
