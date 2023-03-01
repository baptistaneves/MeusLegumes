namespace MeusLegumes.Application.Contexts.Impostos.Validations;

internal class ActualizarMotivoIsencaoIvaValidation : AbstractValidator<ActualizarMotivoIsencaoIva>
{
    public ActualizarMotivoIsencaoIvaValidation()
    {
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty).WithMessage("O Id do imposto informado não é válido");

        RuleFor(c => c.CodigoInterno)
           .NotEmpty().WithMessage("O código intero do motivo deve ser informado");

        RuleFor(c => c.MencaoFactura)
           .NotEmpty().WithMessage("A menção pela factura deve ser informada");

        RuleFor(c => c.NormaLegalAplicavel)
           .NotEmpty().WithMessage("A norma legal aplicavel deve ser informada");

        RuleFor(c => c.Motivo)
           .NotEmpty().WithMessage("Informe o motivo");
    }
}
