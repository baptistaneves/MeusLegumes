namespace MeusLegumes.Infrastructure.Repositories.Produtos;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(ApplicationContext context) : base(context)
    {
    }

    public async Task<Produto> ObterProdutoComPacotes(Guid id)
    {
        return await _context.Produtos.AsNoTracking().Include(p => p.PacotesProduto).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Produto> ObterProdutoComImagensProdutos(Guid id)
    {
        return await _context.Produtos.AsNoTracking().Include(p => p.ProdutosImagem).Include(p=> p.ProdutosRelacionado).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<bool> ExisteRelacaoComProduto(Guid id, Guid produtoRelacionadoId)
    {
        return await _context.ProdutoRelacionados.AsNoTracking().AnyAsync(p => p.ProdutoId == id && p.Id == produtoRelacionadoId);
    }

    public void AdicionarProdutoImagem(ProdutoImagem produtoImagem)
    {
        _context.ProdutoImagens.Add(produtoImagem);
    }

    public void AdicionarProdutoRelacionado(ProdutoRelacionado produtoRelacionado)
    {
        _context.ProdutoRelacionados.Add(produtoRelacionado);
    }
}
