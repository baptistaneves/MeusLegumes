namespace MeusLegumes.Domain.Contexts.Produtos.Entities;

public class ProdutoRelacionado : Entity
{
    public Guid ProdutoId { get; private set; }
    public Guid ProdutoRelacionadoId { get; private set; }

    public ProdutoRelacionado(Guid produtoId, Guid produtoRelacionadoId)
    {
        ProdutoId = produtoId;
        ProdutoRelacionadoId = produtoRelacionadoId;
    }

    //EF Rel.
    public Produto Produto { get; private set; }
}
