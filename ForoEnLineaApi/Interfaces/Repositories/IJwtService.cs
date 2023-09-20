using System.Security.Claims;

namespace ForoEnLineaApi.Interfaces.Repositories
{
    public interface IJwtService
    {
        string Generate(Claim[] claims, DateTime? expiresUtc = null, string audience = null);
    }
}
