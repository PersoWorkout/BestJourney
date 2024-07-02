using API.Attributes;
using API.Extensions;
using Application.Orders;
using Microsoft.AspNetCore.Mvc;

namespace API.Orders.GetOrderByUser;

[ApiController]
[Route("/orders")]
public class GetOrdersByUserController(
    IOrderService service,
    GetOrdersByUserPresenter presenter) : Controller
{
    private readonly IOrderService _service = service;
    private readonly GetOrdersByUserPresenter _presenter = presenter;

    [Authenticated]
    [HttpGet]
    public async Task<IResult> Handle()
    {
        var result = await _service.GetByUser(
                HttpContext.Items["userId"]!.ToString()!);

        return result.IsSucess ?
            Results.Ok(_presenter.ToJson(result.Data)) :
            ResultExtensions.FailureResult(result);
    }
}
