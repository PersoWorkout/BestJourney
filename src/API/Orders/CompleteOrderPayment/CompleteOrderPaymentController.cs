using API.Attributes;
using API.Extensions;
using Application.Orders;
using Microsoft.AspNetCore.Mvc;

namespace API.Orders.CompleteOrderPayment;

[ApiController]
[Route("/orders/payment/complete")]
public class CompleteOrderPaymentController(
    IOrderService orderService, 
    CompleteOrderPaymentPresenter presenter) : Controller
{ 
    private readonly IOrderService _service = orderService;
    private readonly CompleteOrderPaymentPresenter _presenter = presenter;

    [Authenticated]
    [HttpPut("{orderId}")]
    public async Task<IResult> Handle(string orderId)
    {
        var result = await _service.CompletePayment(
            orderId, 
            HttpContext.Items["userId"]!.ToString()!);

        return result.IsSucess ?
            Results.Ok(_presenter.ToJson(result.Data!)) :
            ResultExtensions.FailureResult(result);
    }
}
