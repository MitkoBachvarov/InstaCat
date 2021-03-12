using System.Linq;
using System.Security.Claims;

namespace Catstagram.Server.Infrastructure
{
    public static class IdentityExtensions
    {

        public static string GetId(this ClaimsPrincipal user)
            => user
                .Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                ?.Value;

        
    }
}
