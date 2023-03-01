namespace MeusLegumes.Application.Contexts.Enderecos.Validations;

internal class CriarProvinciaValidation : AbstractValidator<CriarProvincia>
{
	public CriarProvinciaValidation()
	{
        RuleFor(c => c.Nome)
            .NotEmpty().WithMessage("O Nome da província deve ser informado")
            .MinimumLength(4).WithMessage("O Nome da província deve ter no minimo 4 caracteres");
    }
}
