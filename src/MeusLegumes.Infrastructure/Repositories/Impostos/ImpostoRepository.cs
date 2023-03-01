namespace MeusLegumes.Infrastructure.Repositories.Impostos;

public class ImpostoRepository : Repository<Imposto>, IImpostoRepository
{
    public ImpostoRepository(ApplicationContext context) : base(context)
    {
    }

    public async Task<Imposto> VerificarSeImpostoPossuiProdutosPorId(Guid id)
    {
        return await _context.Impostos.AsNoTracking().Include(i => i.Produtos).FirstOrDefaultAsync(c => c.Id == id);
    }
}
