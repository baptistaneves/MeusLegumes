namespace MeusLegumes.Application.Contexts.Identity.Commands;

public class CriarUsuarioCommand : Command<IdentityResponse>
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string Perfil { get; private set; }
    public CriarUsuarioCommand(string name, string email, string password, string perfil)
    {
        Name = name;
        Email = email;
        Password = password;
        Perfil = perfil;
    }

    public override bool IsValid()
    {
        var validationResult = new CriarUsuarioValidation().Validate(this);
        return validationResult.IsValid;
    }
}
