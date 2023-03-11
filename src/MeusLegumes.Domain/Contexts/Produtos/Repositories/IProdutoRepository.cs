namespace MeusLegumes.Domain.Contexts.Produtos.Repositories;

public interface IProdutoRepository : IRepository<Produto>
{
    Task<Produto> ObterProdutosComPacotes(Guid id);
}
