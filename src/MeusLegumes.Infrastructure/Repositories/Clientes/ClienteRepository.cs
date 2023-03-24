namespace MeusLegumes.Infrastructure.Repositories.Clientes;

public class ClienteRepository : Repository<Cliente>, IClienteRepository
{
    public ClienteRepository(ApplicationContext context) : base(context) {}

    public async Task<Cliente> OterClientePorUserIdentityId(string userIdentityId)
    {
        return await _context.Clientes.AsNoTracking().FirstOrDefaultAsync(p => p.UserIdentityId== userIdentityId);  
    }
}
