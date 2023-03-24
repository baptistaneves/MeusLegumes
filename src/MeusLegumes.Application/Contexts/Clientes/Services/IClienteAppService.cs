namespace MeusLegumes.Application.Contexts.Clientes.Services;

public interface IClienteAppService : IDisposable
{
    Task Adicionar(CriarCliente cliente, string identityUserId, CancellationToken cancellationToken);
    Task Actualizar(ActualizarCliente cliente, CancellationToken cancellationToken);
    Task Remover(Guid id, CancellationToken cancellationToken);
    Task<Cliente> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Cliente>> ObterTodosAsync();
    Task<Cliente> OterClientePorUserIdentityId(string userIdentityId);
}

