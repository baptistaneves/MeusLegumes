namespace MeusLegumes.Application.Contexts.Enderecos.Validations;

internal class ActualizarProvinciaValidation : AbstractValidator<ActualizarProvincia>
{
    public ActualizarProvinciaValidation()
    {

        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty).WithMessage("O Id da unidade é inválido");

        RuleFor(c => c.Nome)
            .NotEmpty().WithMessage("O Nome da província deve ser informado")
            .MinimumLength(4).WithMessage("O Nome da província deve ter no minimo 4 caracteres");
    }
}
