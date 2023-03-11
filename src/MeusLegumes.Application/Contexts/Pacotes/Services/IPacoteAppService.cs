namespace MeusLegumes.Application.Contexts.Pacotes.Services;

public interface IPacoteAppService : IDisposable
{
    Task Adicionar(CriarPacote pacote, CancellationToken cancellationToken);
    Task Actualizar(ActualizarPacote pacote, CancellationToken cancellationToken);
    Task Remover(Guid id, CancellationToken cancellationToken);
    Task<Pacote> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Pacote>> ObterTodosAsync();
}
