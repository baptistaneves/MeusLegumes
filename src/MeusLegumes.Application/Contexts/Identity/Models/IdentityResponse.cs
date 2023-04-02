namespace MeusLegumes.Application.Contexts.Identity.Models;
public class IdentityResponse
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
}
