namespace MeusLegumes.Application.Contexts.Pedidos.Commands;

public class IniciarPedidoCommand : Command<bool>
{
    public Guid PedidoId { get; private set; }
    public Guid ClienteId { get; private set; }
    public decimal Total { get; private set; }

    public IniciarPedidoCommand(Guid pedidoId, Guid clienteId, decimal total)
    {
        PedidoId = pedidoId;
        ClienteId = clienteId;
        Total = total;
    }

    public override bool IsValid()
    {
        ValidationResult = new IniciarPedidoValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class IniciarPedidoValidation : AbstractValidator<IniciarPedidoCommand>
{
    public IniciarPedidoValidation()
    {
        RuleFor(c => c.ClienteId)
            .NotEqual(Guid.Empty)
            .WithMessage("Id do cliente inválido");

        RuleFor(c => c.PedidoId)
            .NotEqual(Guid.Empty)
            .WithMessage("Id do pedido inválido");

        RuleFor(c => c.Total)
           .NotEmpty().WithMessage("O total do pedido não foi informado");
    }
}
