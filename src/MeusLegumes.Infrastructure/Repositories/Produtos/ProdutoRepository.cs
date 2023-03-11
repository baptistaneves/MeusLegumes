namespace MeusLegumes.Infrastructure.Repositories.Produtos;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(ApplicationContext context) : base(context)
    {
    }

    public async Task<Produto> ObterProdutosComPacotes(Guid id)
    {
        return await _context.Produtos.AsNoTracking().Include(p => p.PacotesProduto).FirstOrDefaultAsync(p => p.Id == id);
    }
}
