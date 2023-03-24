namespace MeusLegumes.Application.Contexts.Pedidos.Commands;

public class FinalizarPedidoCommand : Command<bool>
{
    public Guid PedidoId { get; private set; }
    public Guid ClienteId { get; private set; }

    public FinalizarPedidoCommand(Guid pedidoId, Guid clienteId)
    {
        PedidoId = pedidoId;
        ClienteId = clienteId;
    }

    public override bool IsValid()
    {
        return new FinalizarPedidoCommandValidation().Validate(this).IsValid;
    }
}

public class FinalizarPedidoCommandValidation : AbstractValidator<FinalizarPedidoCommand>
{
    public FinalizarPedidoCommandValidation()
    {
        RuleFor(c => c.ClienteId)
            .NotEqual(Guid.Empty).WithMessage("Id do cliente inválido");

        RuleFor(c => c.PedidoId)
            .NotEqual(Guid.Empty).WithMessage("Id do pedido inválido");
    }
}
