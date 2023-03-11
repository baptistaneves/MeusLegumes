namespace MeusLegumes.Application.Contexts.Produtos.Services;

public interface IProdutoAppService : IDisposable
{
    Task Adicionar(CriarProduto produto, CancellationToken cancellationToken);
    Task Actualizar(ActualizarProduto produto, CancellationToken cancellationToken);
    Task Remover(Guid id, CancellationToken cancellationToken);
    Task<Produto> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Produto>> ObterTodosAsync();
}
