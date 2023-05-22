namespace MeusLegumes.Application.Contexts.Identity.Validations;

internal class ActualizarUsuarioValidation : AbstractValidator<ActualizarUsuarioCommand>
{
    public ActualizarUsuarioValidation()
    {
        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("O email deve ser informado")
            .EmailAddress().WithMessage("O email informado não é válido");

        RuleFor(u => u.Name)
           .NotEmpty().WithMessage("O nome deve ser informado");

        RuleFor(u => u.Perfil)
           .NotEmpty().WithMessage("O perfil deve ser informado");
    }
}
