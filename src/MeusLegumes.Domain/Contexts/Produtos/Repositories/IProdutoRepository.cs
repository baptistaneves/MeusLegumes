namespace MeusLegumes.Domain.Contexts.Produtos.Repositories;

public interface IProdutoRepository : IRepository<Produto>
{
    Task<Produto> ObterProdutoComImagensProdutos(Guid id);
    Task<bool> ExisteRelacaoComProduto(Guid id, Guid produtoRelacionadoId);
    void AdicionarProdutoImagem(ProdutoImagem produtoImagem);
    void AdicionarProdutoRelacionado(ProdutoRelacionado produtoRelacionado);

    Task<Produto> ObterPacotePorId(Guid id);
    Task<IEnumerable<Produto>> ObterTodosPacotes();

    Task<Produto> ObterProdutoPorId(Guid id);
    Task<IEnumerable<Produto>> ObterTodosProdutos();
}
