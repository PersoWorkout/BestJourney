using API.Attributes;
using API.Extensions;
using Application.Orders;
using Domain.Orders.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Orders.UpdateOrder;

[ApiController]
[Route("/orders")]
public class UpdateOrderController(
    IOrderService service,
    UpdateOrderPresenter presenter) : Controller
{
    private readonly IOrderService _service = service;
    private readonly UpdateOrderPresenter _presenter = presenter;

    [Authenticated]
    [HttpPut("{id}")]
    public async Task<IResult> Index(string id, [FromBody] UpdateOrderRequest payload)
    {
        var result = await _service.Update(
            id,
            HttpContext.Items["userId"]!.ToString()!,
            payload);

        return result.IsSucess ?
            Results.Ok(_presenter.ToJson(result.Data!)) :
            ResultExtensions.FailureResult(result);
    }
}
