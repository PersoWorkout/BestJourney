using API.Attributes;
using Application.Users;
using Application.Users.Customers;
using Microsoft.AspNetCore.Mvc;

namespace API.Users
{
    [ApiController]
    [Route("/users")]
    public class UserController(
        ICustomerService userService) : Controller
    {
        private readonly ICustomerService _userService = userService;

        [HttpGet]
        public async Task<IResult> Get()
        {
            var result = await _userService.GetAll();
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
        [HttpGet("/users/me")]
        public async Task<IResult> Me()
        {

            var userId = HttpContext.Items["userId"]?.ToString();
            if (userId is null) return Results.BadRequest();

            var result = await _userService.GetById(userId);

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
    }
}
