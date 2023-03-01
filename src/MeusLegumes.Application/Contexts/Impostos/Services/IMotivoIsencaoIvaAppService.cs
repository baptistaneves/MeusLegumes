namespace MeusLegumes.Application.Contexts.Impostos.Services;

public interface IMotivoIsencaoIvaAppService : IDisposable
{
    Task Adicionar(CriarMotivoIsencaoIva motivo, CancellationToken cancellationToken);
    Task Actualizar(ActualizarMotivoIsencaoIva motivo, CancellationToken cancellationToken);
    Task Remover(Guid id, CancellationToken cancellationToken);
    Task<MotivoIsencaoIva> ObterPorIdAsync(Guid id);
    Task<IEnumerable<MotivoIsencaoIva>> ObterTodosAsync();
}

