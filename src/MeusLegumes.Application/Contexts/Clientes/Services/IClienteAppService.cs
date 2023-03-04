namespace MeusLegumes.Application.Contexts.Clientes.Services;

public interface IClienteAppService : IDisposable
{
    Task Adicionar(CriarCliente cliente, CancellationToken cancellationToken);
    Task Actualizar(ActualizarCliente cliente, CancellationToken cancellationToken);
    Task Remover(Guid id, CancellationToken cancellationToken);
    Task<Cliente> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Cliente>> ObterTodosAsync();
}

