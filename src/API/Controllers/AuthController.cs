using Application.Auth;
using Domain.Auth.Validators;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/auth")]
    public class AuthController(
        IAuthService authService) : Controller
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("/auth/register")]
        public async Task<IResult> Register(CreateUserValidator payload)
        {
            var result = await _authService
                .Register(payload);

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

        [HttpPost("/auth/login")]
        public async Task<IResult> Login(LoginUserRequest payload)
        {
            var result = await _authService
                .Login(payload);

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

        [HttpDelete]
        public async Task<IResult> Logout()
        {
            var token = HttpContext.Request
                .Headers.Authorization.ToString();

            await _authService.Logout(token);

            return Results.NoContent();
        }
    }
}
