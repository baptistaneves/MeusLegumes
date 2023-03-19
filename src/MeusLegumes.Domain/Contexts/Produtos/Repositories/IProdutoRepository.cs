namespace MeusLegumes.Domain.Contexts.Produtos.Repositories;

public interface IProdutoRepository : IRepository<Produto>
{
    Task<Produto> ObterProdutoComPacotes(Guid id);
    Task<Produto> ObterProdutoComImagensProdutos(Guid id);
    Task<bool> ExisteRelacaoComProduto(Guid id, Guid produtoRelacionadoId);
    void AdicionarProdutoImagem(ProdutoImagem produtoImagem);
    void AdicionarProdutoRelacionado(ProdutoRelacionado produtoRelacionado);
}
