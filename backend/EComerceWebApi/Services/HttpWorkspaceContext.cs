using Application.Interfaces;
using Application.Security;
using System.Security.Claims;

namespace EComerceWebApi.Services
{
    public class HttpWorkspaceContext : IWorkspaceContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpWorkspaceContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? WorkspaceId
        {
            get
            {
                var value = _httpContextAccessor.HttpContext?.User
                    .FindFirstValue(WorkspaceClaimTypes.WorkspaceId);

                return Guid.TryParse(value, out var workspaceId) ? workspaceId : null;
            }
        }
    }
}
