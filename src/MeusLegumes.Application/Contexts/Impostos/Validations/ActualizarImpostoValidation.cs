namespace MeusLegumes.Application.Contexts.Impostos.Validations;

internal class ActualizarImpostoValidation : AbstractValidator<ActualizarImposto>
{
    public ActualizarImpostoValidation()
    {
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty).WithMessage("O Id do imposto informado não é válido");

        RuleFor(c => c.Descricao)
             .NotEmpty().WithMessage("A descrição do imposto deve ser informado");

        RuleFor(c => c.Taxa)
           .NotEmpty().WithMessage("A taxa do imposto deve ser informada");

        RuleFor(c => c.TipoDeTaxa)
           .NotEmpty().WithMessage("O tipo de taxa do imposto deve ser informado");
    }
}
