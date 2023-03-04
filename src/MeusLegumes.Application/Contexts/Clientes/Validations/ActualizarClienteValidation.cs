namespace MeusLegumes.Application.Contexts.Clientes.Validations;

internal class ActualizarClienteValidation : AbstractValidator<ActualizarCliente>
{
    public ActualizarClienteValidation()
    {
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty).WithMessage("O Id do cliente não é válido");

        RuleFor(c => c.Nome)
        .NotEmpty().WithMessage("O nome deve ser informado");

        RuleFor(c => c.MunicipioId)
            .NotEqual(Guid.Empty).WithMessage("O Id do munícipio é inválido ou não foi informado");

        RuleFor(c => c.Tipo)
            .NotEmpty().WithMessage("Selecione o tipo de cliente");

        RuleFor(c => c.NumeroContribuinte)
            .NotEmpty().WithMessage("O Nº de Contribuinte deve ser informado");

        RuleFor(c => c.TelefonePrincipal)
            .NotEmpty().WithMessage("O telefone principal deve ser informado");

        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("O email deve ser informado")
            .EmailAddress().WithMessage("O email informado é inválido");

        RuleFor(c => c.Rua)
            .NotEmpty().WithMessage("A Rua deve ser deve ser informada");

        RuleFor(c => c.PontoDeReferencia)
            .NotEmpty().WithMessage("O ponto de referência deve ser informado");
    }
}
