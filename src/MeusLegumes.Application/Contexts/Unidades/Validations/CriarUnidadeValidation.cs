namespace MeusLegumes.Application.Contexts.Unidades.Validations;

internal class CriarUnidadeValidation : AbstractValidator<CriarUnidade>
{
	public CriarUnidadeValidation()
	{
        RuleFor(c => c.Descricao)
            .NotEmpty().WithMessage("A Descrição/Nome da unidade deve ser informado")
            .MinimumLength(4).WithMessage("A Descrição/Nome da unidade deve ter no minimo 4 caracteres");
    }
}
