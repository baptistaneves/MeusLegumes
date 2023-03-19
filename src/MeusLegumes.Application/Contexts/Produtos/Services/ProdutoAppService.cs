namespace MeusLegumes.Application.Contexts.Produtos.Services;

public class ProdutoAppService : BaseService, IProdutoAppService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMapper _mapper;
    public ProdutoAppService(INotifier notifier, IProdutoRepository produtoRepository, IMapper mapper) : base(notifier)
    {
        _produtoRepository = produtoRepository;
        _mapper = mapper;
    }
    public async Task Adicionar(CriarProduto produto, CancellationToken cancellationToken)
    {
        if (!Validate(new CriarProdutoValidation(), produto)) return;

        if (_produtoRepository.BuscarAsync(c => c.Nome == produto.Nome).Result.Any())
        {
            Notify(ProdutoErrorMessages.ProdutoJaExiste);
            return;
        }

        var novoProduto = _mapper.Map<Produto>(produto);

        if(produto.ProdutosRelacionados.Any())
        {
            foreach (var produtoRelacionado in produto.ProdutosRelacionados)
            {
                novoProduto.AdicionarProdutoRelacionado(new ProdutoRelacionado(produtoRelacionado));
            }
        }

        if (produto.ImagensOpcionaisUrls.Any())
        {
            foreach (var imagemUrl in produto.ImagensOpcionaisUrls)
            {
                novoProduto.AdicionarProdutoImagem(new ProdutoImagem(imagemUrl));
            }
        }

        _produtoRepository.Adicionar(novoProduto);

        await _produtoRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<Produto> Actualizar(ActualizarProduto actProduto, CancellationToken cancellationToken)
    {
        if (!Validate(new ActualizarProdutoValidation(), actProduto)) return null;

        var produto = await _produtoRepository.ObterPorIdAsync(actProduto.Id);

        if (produto is null)
        {
            Notify(ProdutoErrorMessages.ProdutoNaoEncotrado);
            return null;
        }

        var produtoAntigo = produto;

        if (_produtoRepository.BuscarAsync(c => c.Nome == actProduto.Nome && c.Id != actProduto.Id).Result.Any())
        {
            Notify(ProdutoErrorMessages.ProdutoJaExiste);
            return null;
        }

        produto.ActualizarProduto(actProduto.CategoriaId, actProduto.UnidadeId, actProduto.ImpostoId, actProduto.MotivoId, actProduto.Nome, actProduto.Descricao, actProduto.PrecoUnitario, actProduto.UrlImagemPrincipal, actProduto.EmPromocao, actProduto.PrecoPromocional, actProduto.Destaque, actProduto.NovoLancamento, actProduto.MaisVendido, actProduto.MaisProcurado, actProduto.EmEstoque, actProduto.Activo, actProduto.Observacao);

        if (actProduto.ProdutosRelacionados.Any())
        {
            foreach (var produtoRelacionadoId in actProduto.ProdutosRelacionados)
            {
                if(!await _produtoRepository.ExisteRelacaoComProduto(produto.Id, produtoRelacionadoId)) 
                {
                    var produtoRelacionado = new ProdutoRelacionado(produtoRelacionadoId);
                    produtoRelacionado.AssociarAoProduto(actProduto.Id);
                    _produtoRepository.AdicionarProdutoRelacionado(produtoRelacionado);
                }
            }
        }

        if (actProduto.ImagensOpcionaisUrls.Any())
        {
            foreach (var imagemUrl in actProduto.ImagensOpcionaisUrls)
            {
                var produtoImagem = new ProdutoImagem(imagemUrl);
                produtoImagem.AssociarAoProduto(produto.Id);
                _produtoRepository.AdicionarProdutoImagem(produtoImagem);
            }
        }

        _produtoRepository.Actualizar(produto);
        await _produtoRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return produtoAntigo;
    }

    public async Task Remover(Guid id, CancellationToken cancellationToken)
    {
        if (_produtoRepository.ObterProdutoComPacotes(id).Result.PacotesProduto.Any())
        {
            Notify(ProdutoErrorMessages.ProdutoNaoPodeSerRemovido);
            return;
        }

        _produtoRepository.Remover(id);
        await _produtoRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<Produto> ObterPorIdAsync(Guid id)
    {
        return await _produtoRepository.ObterPorIdAsync(id);
    }

    public async Task<Produto> ObterProdutoComImagensProdutos(Guid id)
    {
        return await _produtoRepository.ObterProdutoComImagensProdutos(id);
    }

    public async Task<IEnumerable<Produto>> ObterTodosAsync()
    {
        return await _produtoRepository.ObterTodosAsync();
    }

    public void Dispose()
    {
        _produtoRepository?.Dispose();
    }

}
