using API.Extensions;
using Application.Auth;
using Domain.Auth.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Auth;

[ApiController]
[Route("auth/login")]
public class LoginController(IAuthService service) : Controller
{
    private readonly IAuthService _authService = service;

    [HttpPost]
    public async Task<IResult> Handle(LoginUserRequest payload)
    {
        var result = await _authService.Login(payload);

        return result.IsSucess ?
            Results.Ok(result.Data) :
            ResultExtensions.FailureResult(result);
    }
}
