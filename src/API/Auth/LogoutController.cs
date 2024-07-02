using Application.Auth;
using Microsoft.AspNetCore.Mvc;

namespace API.Auth;

[ApiController]
[Route("/auth")]
public class LogoutController(IAuthService service) : Controller
{
    private readonly IAuthService _service = service;

    [HttpDelete]
    public async Task<IResult> Handle()
    {
        var token = HttpContext.Request
            .Headers.Authorization.ToString();

        await _service.Logout(token);

        return Results.NoContent();
    }
}
