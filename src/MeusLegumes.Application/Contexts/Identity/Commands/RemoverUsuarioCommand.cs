namespace MeusLegumes.Application.Contexts.Identity.Commands;

public class RemoverUsuarioCommand : Command<bool>
{
    public Guid Id { get; private set; }

    public RemoverUsuarioCommand(Guid id)
    {
        Id = id;
    }
}
