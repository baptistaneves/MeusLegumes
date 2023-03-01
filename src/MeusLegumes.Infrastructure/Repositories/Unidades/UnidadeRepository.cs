namespace MeusLegumes.Infrastructure.Repositories.Unidades;

public class UnidadeRepository : Repository<Unidade>, IUnidadeRepository
{
    public UnidadeRepository(ApplicationContext context) : base(context) { }

    public async Task<Unidade> VerificarSeUnidadePossuiProdutosPorId(Guid id)
    {
        return await _context.Unidades.AsNoTracking().Include(u => u.Produtos).FirstOrDefaultAsync(c => c.Id == id);
    }
}
