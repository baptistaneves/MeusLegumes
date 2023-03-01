namespace MeusLegumes.Domain.Contexts.Produtos.Repositories;

public interface IProdutoRepository : IRepository<Produto>
{
    Task<IEnumerable<ProdutoImagem>> ObterProdutoImagensPorProdutoId(Guid produtoId);
    Task<IEnumerable<ProdutoImagem>> ObterProdutoRelacionadosPorProdutoId(Guid produtoId);
}
