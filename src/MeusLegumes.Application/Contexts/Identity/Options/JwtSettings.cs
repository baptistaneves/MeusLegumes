namespace MeusLegumes.Application.Contexts.Identity.Options;

public class JwtSettings
{
    public string SigningKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}
