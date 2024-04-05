using System.Security.Claims;

namespace PomocKolezenska.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? GetClaim(this ClaimsPrincipal claimsPrincipal, string claim)
    {
        return claimsPrincipal.FindFirstValue(claim);
    }
}
