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

    public async Task<Produto> ObterProdutoPorId(Guid id)
    {
        return await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id && p.Tipo == TipoProduto.Produto);

    }

    public async Task<IEnumerable<ProdutoDto>> ObterTodosProdutos()
    {
        return await _context.Produtos.AsNoTracking()
            .Where(p => p.Tipo == TipoProduto.Produto)
            .Include(p => p.Categoria)
            .Include(p => p.Unidade)
            .Include(p => p.Imposto)
            .Include(p => p.MotivoIsencaoIva)
            .Select(produto => new ProdutoDto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                CategoriaId = produto.CategoriaId,
                UnidadeId = produto.UnidadeId,
                MotivoId = produto.MotivoId,
                ImpostoId = produto.ImpostoId,
                CategoriaDescricao = produto.Categoria.Descricao,
                UnidadeDescricao = produto.Unidade.Descricao,
                MotivoDescricao = produto.MotivoIsencaoIva.Motivo,
                ImpostoDescricao = produto.Imposto.Descricao,
                Descricao = produto.Descricao,
                PrecoPromocional = produto.PrecoPromocional,
                PrecoUnitario = produto.PrecoUnitario,
                UrlImagemPrincipal = produto.UrlImagemPrincipal,
                EmPromocao = produto.EmPromocao,
                NovoLancamento = produto.NovoLancamento,
                EmEstoque = produto.EmEstoque,
                Destaque = produto.Destaque,
                MaisProcurado = produto.MaisProcurado,
                MaisVendido = produto.MaisVendido,
                Observacao = produto.Observacao,
                Activo = produto.Activo
            })
            .ToListAsync();

    }

    public async Task<IEnumerable<PacoteDto>> ObterTodosPacotes()
    {
        return await _context.Produtos.AsNoTracking()
            .Where(p => p.Tipo == TipoProduto.Pacote)
            .Include(p => p.Unidade)
            .Select(pacote => new PacoteDto
            {
                Id = pacote.Id,
                Nome = pacote.Nome,
                UnidadeId = pacote.UnidadeId,
                UnidadeDescricao = pacote.Unidade.Descricao,
                Descricao = pacote.Descricao,
                PrecoPromocional = pacote.PrecoPromocional,
                PrecoUnitario = pacote.PrecoUnitario,
                UrlImagemPrincipal = pacote.UrlImagemPrincipal,
                EmPromocao = pacote.EmPromocao,
                Activo = pacote.Activo
            })
            .ToListAsync();
    }
}
