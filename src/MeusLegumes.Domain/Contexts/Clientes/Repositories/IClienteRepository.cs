namespace MeusLegumes.Domain.Contexts.Clientes.Repositories;

public interface IClienteRepository : IRepository<Cliente> 
{
    Task<Cliente> OterClientePorUserIdentityId(string userIdentityId);
}
