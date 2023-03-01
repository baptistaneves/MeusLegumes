namespace MeusLegumes.Infrastructure.Repositories.Clientes;

public class ClienteRepository : Repository<Cliente>, IClienteRepository
{
    public ClienteRepository(ApplicationContext context) : base(context) {}
}
