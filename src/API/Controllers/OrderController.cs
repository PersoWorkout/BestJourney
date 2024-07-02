using API.Presenter.Orders;
using Application.Orders;
using Domain.DTOs.Validators.Orders;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/orders")]
    public class OrderController(
        IOrderService orderService, 
        CreateOrderPresenter createOrderPresenter): Controller
    {
        private readonly IOrderService _orderService = orderService;
        private readonly CreateOrderPresenter _createOrderPresenter = createOrderPresenter;

        [Authenticated]
        [HttpGet]
        public async Task<IResult> GetByUser()
        {
            var result = await _orderService.GetByUser(
                HttpContext.Items["userId"].ToString());

            return result.IsSucess ?
                Results.Ok(result.Data) :
                Results.Problem(
                    statusCode: StatusCodes.Status400BadRequest,
                    title: "Bad Request",
                    extensions: new Dictionary<string, object?>
                    {
                        {"errors", new [] {result.Error } }
                    });
        }

        [Authenticated]
        [HttpPost]
        public async Task<IResult> Create([FromBody] CreateOrderValidator payload)
        {
            var result = await _orderService.Create(
                payload, 
                HttpContext.Items["userId"]!.ToString()!);

            return result.IsSucess ?
                Results.Ok(_createOrderPresenter.ToJson(result.Data!)) :
                Results.Problem(
                    statusCode: StatusCodes.Status400BadRequest,
                    title: "Bad Request",
                    extensions: new Dictionary<string, object?>
                    {
                        {"errors", new [] {result.Error } }
                    });
        }
    }
}
