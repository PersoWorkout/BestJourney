using API.Attributes;
using API.Extensions;
using Application.Orders;
using Microsoft.AspNetCore.Mvc;

namespace API.Orders.BeginOrderPayment;

[ApiController]
[Route("/orders/payment/begin")]
public class BeginOrderPaymentController(
    IOrderService service,
    BeginOrderPaymentPresenter presenter) : Controller
{
    private readonly IOrderService _service = service;
    private readonly BeginOrderPaymentPresenter _presenter = presenter;

    [Authenticated]
    [HttpPut("{orderId}")]
    public async Task<IResult> Handle(string orderId)
    {
        var result = await _service.BeginPayment(
            orderId, 
            HttpContext.Items["userId"]!.ToString()!);

        return result.IsSucess ?
            Results.Ok(_presenter.ToJson(result.Data!)) :
            ResultExtensions.FailureResult(result);
    }
}
