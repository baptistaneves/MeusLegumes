namespace MeusLegumes.Infrastructure.Repositories.Categorias;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{

    public CategoriaRepository(ApplicationContext context) : base(context) { }

    public async Task<Categoria> VerificarSeCategoriaPossuiProdutosPorId(Guid id)
    {
       return await _context.Categorias.AsNoTracking().Include(c => c.Produtos).FirstOrDefaultAsync(c => c.Id == id);
    }
}
