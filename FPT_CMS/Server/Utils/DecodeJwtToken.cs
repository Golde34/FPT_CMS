using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Server.Utils;

public static class DecodeJwtToken
{
    public static string? GetRoleFromToken(string? token)
    {
        if (token == null)
        {
            return null;
        }
        AuthenticationHeaderValue.TryParse(token, out var tokenParse);
        var handler = new JwtSecurityTokenHandler();
        var tokenDecode = handler.ReadJwtToken(token);
        return tokenDecode.Claims.First(p => p.Type.Equals(ClaimTypes.Role)).Value;
    }
}