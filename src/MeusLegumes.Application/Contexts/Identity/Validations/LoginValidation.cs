namespace MeusLegumes.Application.Contexts.Identity.Validations;

internal class LoginValidation : AbstractValidator<LoginCommand>
{
    public LoginValidation()
    {
        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("O email deve ser informado")
            .EmailAddress().WithMessage("O email informado não é válido");

        RuleFor(u => u.Password)
           .NotEmpty().WithMessage("A senha deve ser informada");
    }
}
