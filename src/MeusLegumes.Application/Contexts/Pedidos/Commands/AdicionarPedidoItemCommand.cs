namespace MeusLegumes.Application.Contexts.Pedidos.Commands;

public class AdicionarPedidoItemCommand : Command<bool>
{
    public Guid ClienteId { get; private set; }

    public Guid ProdutoId { get; private set; }
    public string Nome { get; private set; }
    public int Quantidade { get; private set; }
    public decimal ValorUnitario { get; private set; }

    public AdicionarPedidoItemCommand(Guid clienteId, Guid produtoId, string nome, int quantidade, 
        decimal valorUnitario)
    {
        ClienteId = clienteId;
        ProdutoId = produtoId;
        Nome = nome;
        Quantidade = quantidade;
        ValorUnitario = valorUnitario;
    }

    public override bool IsValid()
    {
        return new AdicionarPedidoItemValidation().Validate(this).IsValid;
    }
}

public class AdicionarPedidoItemValidation : AbstractValidator<AdicionarPedidoItemCommand>
{
    public AdicionarPedidoItemValidation()
    {
        RuleFor(c => c.ClienteId)
            .NotEqual(Guid.Empty).WithMessage("Id do cliente inválido");

        RuleFor(c => c.ProdutoId)
            .NotEqual(Guid.Empty).WithMessage("Id do produto inválido");

        RuleFor(c => c.Nome)
            .NotEmpty().WithMessage("O nome do produto não foi informado");

        RuleFor(c => c.Quantidade)
            .NotEmpty().WithMessage("A quantidade do produto não foi informada")
            .GreaterThan(0).WithMessage("A quantidade miníma de um item é 1");

        RuleFor(c => c.ValorUnitario)
            .NotEmpty().WithMessage("O valor unitário do produto não foi informado")
            .GreaterThan(0).WithMessage("O valor do item precisa ser maior que 0");
    }
}
