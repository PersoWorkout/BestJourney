using API.Extensions;
using Application.Auth;
using Domain.Auth.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Auth.Suppliers;

[ApiController]
[Route("auth/suppliers/login")]
public class LoginSupplierController(IAuthService service): Controller
{
    private readonly IAuthService _service = service;

    [HttpPost]
    public async Task<IResult> Handle(LoginRequest payload)
    {
        var result = await _service.LoginSupplier(payload);

        return result.IsSucess ?
            Results.Ok(result.Data) :
            ResultExtensions.FailureResult(result);
    }
}
