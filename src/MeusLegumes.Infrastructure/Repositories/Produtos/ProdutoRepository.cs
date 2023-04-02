using Microsoft.EntityFrameworkCore;

namespace MeusLegumes.Infrastructure.Repositories.Produtos;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(ApplicationContext context) : base(context)
    {
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

    public async Task<Produto> ObterPacotePorId(Guid id)
    {
        return await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id && p.Tipo == TipoProduto.Pacote);
    }

    public async Task<IEnumerable<Produto>> ObterTodosPacotes()
    {
        return await _context.Produtos.AsNoTracking().Where(p => p.Tipo == TipoProduto.Pacote).ToListAsync();
    }

    public async Task<Produto> ObterProdutoPorId(Guid id)
    {
        return await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id && p.Tipo == TipoProduto.Produto);

    }

    public async Task<IEnumerable<Produto>> ObterTodosProdutos()
    {
        return await _context.Produtos.AsNoTracking().Where(p => p.Tipo == TipoProduto.Produto).ToListAsync();

    }
}
