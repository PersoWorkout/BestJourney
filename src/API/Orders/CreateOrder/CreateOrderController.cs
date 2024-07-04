using API.Attributes;
using API.Extensions;
using Application.Orders;
using Domain.Orders.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Orders.CreateOrder;

[ApiController]
[Route("/orders")]
public class CreateOrderController(
    IOrderService service,
    CreateOrderPresenter presenter) : Controller
{
    private readonly IOrderService _service = service;
    private readonly CreateOrderPresenter _presenter = presenter;

    [Authenticated]
    [HttpPost]
    public async Task<IResult> Handle([FromBody] CreateOrderRequest payload)
    {
        var result = await _service.Create(
            HttpContext.Items["userId"]!.ToString()!,
            payload);

        return result.IsSucess ?
            Results.Ok(_presenter.ToJson(result.Data!)) :
            ResultExtensions.FailureResult(result);
    }
}