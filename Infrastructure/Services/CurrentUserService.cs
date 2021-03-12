using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Catstagram.Server.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly ClaimsPrincipal user;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
            => user = httpContextAccessor.HttpContext?.User;

        public string GetId()
            => user?.GetId();

        public string GetUserName()
            => user?.Identity?.Name;
    }
}
