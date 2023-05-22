namespace MeusLegumes.Application.Contexts.Identity.Commands;

public class AlterarSenhaCommand : Command<bool>
{
    public Guid Id { get; private set; }
    public string SenhaActual { get; private set; }
    public string NovaSenha { get; private set; }


    public AlterarSenhaCommand(Guid id, string senhaActual, string novaSenha)
    {
        Id = id;
        SenhaActual = senhaActual;
        NovaSenha = novaSenha;
    }

    public override bool IsValid()
    {
        var validationResult = new AlterarSenhaValidation().Validate(this);
        return validationResult.IsValid;
    }
}
