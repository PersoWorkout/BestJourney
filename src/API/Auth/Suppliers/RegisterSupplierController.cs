using API.Extensions;
using Application.Auth;
using Domain.Auth.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Auth.Suppliers;

[ApiController]
[Route("/auth/suppliers/register")]
public class RegisterSupplierController(IAuthService service) : Controller
{
    private readonly IAuthService _authService = service;

    [HttpPost]
    public async Task<IResult> Handle(RegisterSupplierRequest payload)
    {
        var result = await _authService.RegisterSupplier(payload);

        return result.IsSucess ?
            Results.Ok(result.Data) :
            ResultExtensions.FailureResult(result);
    }
}
