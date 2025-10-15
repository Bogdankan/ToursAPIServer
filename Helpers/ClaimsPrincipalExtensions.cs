using System.Security.Claims;

namespace ToursAPI.Helpers;

public static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal user)
    {
        var id = user.FindFirstValue(ClaimTypes.NameIdentifier)
                 ?? user.FindFirstValue("sub") // fallback для JWT
                 ?? throw new UnauthorizedAccessException("No user id claim");

        return id;
    }
}