using API.Extensions;
using Application.Auth;
using Domain.Auth.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Auth.Customers;

[ApiController]
[Route("auth/customers/register")]
public class RegisterController(IAuthService service) : Controller
{
    private readonly IAuthService _authService = service;

    [HttpPost]
    public async Task<IResult> Handle(CreateUserRequest payload)
    {
        var result = await _authService.RegisterCustomer(payload);

        return result.IsSucess ?
            Results.Ok(result.Data) :
            ResultExtensions.FailureResult(result);
    }
}
