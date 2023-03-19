namespace MeusLegumes.Application.Contexts.Identity.Validations;

internal class CriarUsuarioValidation : AbstractValidator<CriarUsuarioCommand>
{
	public CriarUsuarioValidation()
	{
        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("O email deve ser informado")
            .EmailAddress().WithMessage("O email informado não é válido");

        RuleFor(u => u.Name)
           .NotEmpty().WithMessage("O nome deve ser informado");

        RuleFor(u => u.Password)
           .NotEmpty().WithMessage("A senha deve ser informada");

        RuleFor(u => u.Perfil)
           .NotEmpty().WithMessage("O perfil deve ser informado");
    }
}
