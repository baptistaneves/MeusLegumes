namespace MeusLegumes.Infrastructure.Repositories.Impostos;

public class MotivoIsencaoIvaRepository : Repository<MotivoIsencaoIva>, IMotivoIsencaoIvaRepository
{
    public MotivoIsencaoIvaRepository(ApplicationContext context) : base(context)
    {
    }

    public async Task<MotivoIsencaoIva> VerificarSeMotivoPossuiProdutosPorId(Guid id)
    {
        return await _context.MotivosIsencaoIva.AsNoTracking().Include(i => i.Produtos).FirstOrDefaultAsync(c => c.Id == id);
    }
}
