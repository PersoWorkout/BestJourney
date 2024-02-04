using Application.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/users")]
    public class UserController(
        IUserService userService) : Controller
    {
        private readonly IUserService _userService = userService;

        [Authenticated]
        [HttpGet]
        public async Task<IResult> Get()
        {
            var result = await _userService.GetUsers();
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
