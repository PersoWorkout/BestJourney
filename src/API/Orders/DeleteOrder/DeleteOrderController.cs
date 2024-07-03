using API.Attributes;
using API.Extensions;
using Application.Orders;
using Microsoft.AspNetCore.Mvc;

namespace API.Orders.DeleteOrder;

[ApiController]
[Route("/orders")]
public class DeleteOrderController(IOrderService service) : Controller
{
    private readonly IOrderService _service = service;

    [Authenticated]
    [HttpDelete("{id}")]
    public async Task<IResult> Handle(string id)
    {
        var result = await _service.Delete(
            id, 
            HttpContext.Items["userId"]!.ToString()!);

        return result.IsSucess ?
            Results.NoContent() :
            ResultExtensions.FailureResult(result);
    }
}
