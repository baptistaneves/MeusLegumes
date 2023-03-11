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
                novoProduto.AdicionarProdutoRelacionado(new ProdutoRelacionado(produtoRelacionado.ProdutoRelacionadoId));
            }
        }

        if (produto.ProdutoImagens.Any())
        {
            foreach (var produtoImagem in produto.ProdutoImagens)
            {
                novoProduto.AdicionarProdutoImagem(new ProdutoImagem(produtoImagem.UrlImagem));
            }
        }

        _produtoRepository.Adicionar(novoProduto);

        await _produtoRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task Actualizar(ActualizarProduto produtoActualizado, CancellationToken cancellationToken)
    {
        if (!Validate(new ActualizarProdutoValidation(), produtoActualizado)) return;

        if (!_produtoRepository.BuscarAsync(p => p.Id == produtoActualizado.Id).Result.Any())
        {
            Notify(ProdutoErrorMessages.ProdutoNaoEncotrado);
            return;
        }

        if (_produtoRepository.BuscarAsync(c => c.Nome == produtoActualizado.Nome && c.Id != produtoActualizado.Id).Result.Any())
        {
            Notify(ProdutoErrorMessages.ProdutoJaExiste);
            return;
        }

        var produto = _mapper.Map<Produto>(produtoActualizado);

        if (produtoActualizado.ProdutosRelacionados.Any())
        {
            foreach (var produtoRelacionado in produtoActualizado.ProdutosRelacionados)
            {
                produto.AdicionarProdutoRelacionado(new ProdutoRelacionado(produtoRelacionado.ProdutoRelacionadoId));
            }
        }

        if (produtoActualizado.produtoImagens.Any())
        {
            foreach (var produtoImagem in produtoActualizado.produtoImagens)
            {
                produto.AdicionarProdutoImagem(new ProdutoImagem(produtoImagem.UrlImagem));
            }
        }

        _produtoRepository.Actualizar(produto);
        await _produtoRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task Remover(Guid id, CancellationToken cancellationToken)
    {
        if (_produtoRepository.ObterProdutosComPacotes(id).Result.PacotesProduto.Any())
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

    public async Task<IEnumerable<Produto>> ObterTodosAsync()
    {
        return await _produtoRepository.ObterTodosAsync();
    }

    public void Dispose()
    {
        _produtoRepository?.Dispose();
    }
}
