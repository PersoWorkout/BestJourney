using API.Extensions;
using Application.Auth;
using Domain.Auth.Requests.Customers;
using Microsoft.AspNetCore.Mvc;

namespace API.Auth.Customers;

[ApiController]
[Route("auth/customers/register")]
public class RegisterCustomerController(IAuthService service) : Controller
{
    private readonly IAuthService _authService = service;

    [HttpPost]
    public async Task<IResult> Handle(RegisterCustomerRequest payload)
    {
        var result = await _authService.RegisterCustomer(payload);

        return result.IsSucess ?
            Results.Ok(result.Data) :
            ResultExtensions.FailureResult(result);
    }
}
