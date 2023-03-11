namespace MeusLegumes.Domain.Contexts.Produtos.Entities;

public class ProdutoImagem : Entity
{
    public Guid ProdutoId { get; private set; }
    public string UrlImagem { get; private set; }

    public ProdutoImagem(string urlImagem)
    {
        UrlImagem = urlImagem;
    }

    public void AssociarAoProduto(Guid produtoId)
    {
        ProdutoId = produtoId;
    }

    //EF Rel.
    public Produto Produto { get; private set; }
}
