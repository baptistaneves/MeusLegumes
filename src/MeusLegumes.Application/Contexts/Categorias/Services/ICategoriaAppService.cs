namespace MeusLegumes.Application.Contexts.Categorias.Services;

public interface ICategoriaAppService : IDisposable
{
    Task Adicionar(CriarCategoria categoria, CancellationToken cancellationToken);
    Task Actualizar(ActualizarCategoria categoria, CancellationToken cancellationToken);
    Task Remover(Guid id, CancellationToken cancellationToken);
    Task<Categoria> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Categoria>> ObterTodosAsync();
}

