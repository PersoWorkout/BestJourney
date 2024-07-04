using Application.Auth;
using Application.Users;
using Domain.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class IsSupplierAttribute : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var token = context.HttpContext
            .Request
            .Headers
            .Authorization
            .ToString();

        var authService = context.HttpContext
            .RequestServices
            .GetRequiredService<IAuthService>();

        var userId = await authService.IsAuthenticated(token);
        if (!userId.HasValue)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var userRepository = context.HttpContext
            .RequestServices
            .GetRequiredService<IUserRepository>();

        var role = await userRepository.GetRole(userId.Value);

        if(role != UserRole.Supplier)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        context.HttpContext.Items["userId"] = userId.ToString();
    }
}
