namespace MeusLegumes.Application.Contexts.Pedidos.Commands;

public class RemoverPedidoItemCommand : Command<bool>
{
    public Guid ClienteId { get; private set; }
    public Guid ProdutoId { get; private set; }

    public RemoverPedidoItemCommand(Guid clienteId, Guid produtoId)
    {
        ClienteId = clienteId;
        ProdutoId = produtoId;
    }

    public override bool IsValid()
    {
        return new RemoverPedidoItemCommandValidation().Validate(this).IsValid;
    }
}

public class RemoverPedidoItemCommandValidation : AbstractValidator<RemoverPedidoItemCommand>
{
    public RemoverPedidoItemCommandValidation()
    {
        RuleFor(c => c.ClienteId)
            .NotEqual(Guid.Empty).WithMessage("Id do cliente inválido");

        RuleFor(c => c.ProdutoId)
            .NotEqual(Guid.Empty).WithMessage("Id do produto inválido");
    }
}
