using Microsoft.Extensions.Configuration;

namespace Catstagram.Server.Infrastructure.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetDefaultConnectionString(this IConfiguration configuration)
           => configuration.GetConnectionString("DefaultConnection");
    }   
}
