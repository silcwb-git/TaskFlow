using System.Security.Claims;

namespace TaskFlow.Infrastructure.Security
{
    public interface IJwtTokenService
    {
        string GenerateAccessToken(Guid userid, string email);
        string GenerateRefreshToken();
        ClaimsPrincipal? ValidateToken(string token);
    }
}