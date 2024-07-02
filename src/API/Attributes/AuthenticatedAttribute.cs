using Application.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthenticatedAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var token = context.HttpContext
                 .Request
                 .Headers
                 .Authorization
                 .ToString();

            var userId = await context.HttpContext
                .RequestServices
                .GetRequiredService<IAuthService>()
                .IsAuthenticated(token);

            if (userId is null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            context.HttpContext.Items["userId"] = userId.ToString();
        }
    }
}
