using Application.Result;
using Application.Security;

namespace EComerceWebApi.Middlewares
{
    public class WorkspaceClaimGuardMiddleware
    {
        private readonly RequestDelegate _next;

        public WorkspaceClaimGuardMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity?.IsAuthenticated == true &&
                !context.User.HasClaim(claim => claim.Type == WorkspaceClaimTypes.WorkspaceId))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(
                    Result<object>.Failure("Your session is no longer valid. Please sign in again."));
                return;
            }

            await _next(context);
        }
    }
}
