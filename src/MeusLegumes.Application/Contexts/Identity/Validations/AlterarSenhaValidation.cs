namespace MeusLegumes.Application.Contexts.Identity.Validations;

internal class AlterarSenhaValidation : AbstractValidator<AlterarSenhaCommand>
{
    public AlterarSenhaValidation()
    {
        RuleFor(u => u.Id)
            .NotEqual(Guid.Empty).WithMessage("O Id informado não é válido");

        RuleFor(u => u.SenhaActual)
            .NotEmpty().WithMessage("Informe a Senha Actual");

        RuleFor(u => u.NovaSenha)
           .NotEmpty().WithMessage("Informe a Nova Senha");
    }
}
