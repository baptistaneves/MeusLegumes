namespace MeusLegumes.Application.Contexts.Identity.Commands;

public class ActualizarUsuarioCommand : Command<bool>
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Perfil { get; private set; }

    public ActualizarUsuarioCommand(Guid id, string name, string email, string perfil)
    {
        Id  = id;
        Name = name;
        Email = email;
        Perfil = perfil;
    }

    public override bool IsValid()
    {
        var validationResult = new ActualizarUsuarioValidation().Validate(this);
        return validationResult.IsValid;
    }
}
