namespace MeusLegumes.Application.Contexts.Unidades.Services;

public interface IUnidadeAppService : IDisposable
{
    Task Adicionar(CriarUnidade unidade, CancellationToken cancellationToken);
    Task Actualizar(ActualizarUnidade unidade, CancellationToken cancellationToken);
    Task Remover(Guid id, CancellationToken cancellationToken);
    Task<Unidade> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Unidade>> ObterTodosAsync();
}

