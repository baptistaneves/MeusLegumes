namespace MeusLegumes.Application.Contexts.Unidades.Validations;

internal class ActualizarUnidadeValidation : AbstractValidator<ActualizarUnidade>
{
    public ActualizarUnidadeValidation()
    {

        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty).WithMessage("O Id da unidade é inválido");

        RuleFor(c => c.Descricao)
            .NotEmpty().WithMessage("A Descrição/Nome da unidade deve ser informado")
            .MinimumLength(4).WithMessage("A Descrição/Nome da unidade deve ter no minimo 4 caracteres");
    }
}
