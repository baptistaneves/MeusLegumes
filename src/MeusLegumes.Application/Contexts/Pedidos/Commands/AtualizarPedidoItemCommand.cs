namespace MeusLegumes.Application.Contexts.Pedidos.Commands;

public class AtualizarPedidoItemCommand : Command<bool>
{
    public Guid ClienteId { get; private set; }
    public Guid ProdutoId { get; private set; }
    public int Quantidade { get; private set; }

    public AtualizarPedidoItemCommand(Guid clienteId, Guid produtoId, int quantidade)
    {
        ClienteId = clienteId;
        ProdutoId = produtoId;
        Quantidade = quantidade;
    }

    public override bool IsValid()
    {
        return new AtualizarPedidoItemCommandValidation().Validate(this).IsValid;
    }
}

public class AtualizarPedidoItemCommandValidation : AbstractValidator<AtualizarPedidoItemCommand>
{
    public AtualizarPedidoItemCommandValidation()
    {
        RuleFor(c => c.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

        RuleFor(c => c.ProdutoId)
            .NotEqual(Guid.Empty)
            .WithMessage("Id do produto inválido");

        RuleFor(c => c.Quantidade)
            .GreaterThan(0)
            .WithMessage("A quantidade miníma de um item é 1");
    }
}