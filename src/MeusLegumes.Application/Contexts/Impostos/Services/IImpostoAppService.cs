namespace MeusLegumes.Application.Contexts.Impostos.Services;

public interface IImpostoAppService : IDisposable
{
    Task Adicionar(CriarImposto imposto, CancellationToken cancellationToken);
    Task Actualizar(ActualizarImposto imposto, CancellationToken cancellationToken);
    Task Remover(Guid id, CancellationToken cancellationToken);
    Task<Imposto> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Imposto>> ObterTodosAsync();
}

