namespace MeusLegumes.API.Extensions;

public static class HttpContextExtension
{
    public static Guid ObterIdentityUserId(this HttpContext context)
    {
        return Guid.Parse(GetGuidClaimValue("IdentityUserId", context));
    }

    public static string ObterEmail(this HttpContext context)
    {
        return GetGuidClaimValue("Email", context);
    }

    private static string GetGuidClaimValue(string key, HttpContext context)
    {
        var identity = context.User.Identity as ClaimsIdentity;
        return identity?.FindFirst(key)?.Value;
    }
}
