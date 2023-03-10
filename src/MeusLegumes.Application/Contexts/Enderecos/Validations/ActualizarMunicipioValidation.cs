namespace MeusLegumes.Application.Contexts.Enderecos.Validations;

internal class ActualizarMunicipioValidation : AbstractValidator<ActualizarMunicipio>
{
    public ActualizarMunicipioValidation()
    {

        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty).WithMessage("O Id do munícipio é inválido");

        RuleFor(c => c.ProvinciaId)
           .NotEqual(Guid.Empty).WithMessage("O Id da província é inválido ou não foi informado");

        RuleFor(c => c.Nome)
            .NotEmpty().WithMessage("O Nome do munícipio deve ser informado")
            .MinimumLength(4).WithMessage("O Nome do munícipio deve ter no minimo 4 caracteres");
    }
}
