namespace MeusLegumes.Application.Contexts.Produtos.Services;

public class ProdutoAppService : BaseService, IProdutoAppService
{
    private readonly IProdutoRepository _produtoRepository;
    public ProdutoAppService(INotifier notifier, IProdutoRepository produtoRepository) : base(notifier)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task Adicionar(CriarProduto produto, CancellationToken cancellationToken)
    {
        if (!Validate(new CriarProdutoValidation(), produto)) return;

        if (ProdutoJaExiste(produto.Nome)) return;

        var novoProduto = new  Produto(produto.CategoriaId, produto.UnidadeId, produto.ImpostoId, produto.MotivoId, produto.Nome, produto.Descricao, produto.PrecoUnitario, produto.UrlImagemPrincipal, produto.EmPromocao, produto.PrecoPromocional, produto.Destaque, produto.NovoLancamento, produto.MaisVendido, produto.MaisProcurado, produto.EmEstoque, produto.Activo, produto.Observacao);

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

        var produto = await ObterProduto(actProduto.Id);

        if (produto is null) return null;

        var produtoAntigo = produto;

        if (ProdutoJaExiste(actProduto.Nome, actProduto.Id)) return null;

        produto.ActualizarProduto(actProduto.CategoriaId, actProduto.UnidadeId, actProduto.ImpostoId, actProduto.MotivoId, actProduto.Nome, actProduto.Descricao, actProduto.PrecoUnitario, actProduto.UrlImagemPrincipal, actProduto.EmPromocao, actProduto.PrecoPromocional, actProduto.Destaque, actProduto.NovoLancamento, actProduto.MaisVendido, actProduto.MaisProcurado, actProduto.EmEstoque, actProduto.Activo, actProduto.Observacao);

        if (actProduto.ProdutosRelacionados.Any())
        {
            foreach (var produtoRelacionadoId in actProduto.ProdutosRelacionados)
            {
                if(!await _produtoRepository.ExisteRelacaoComProduto(produto.Id, produtoRelacionadoId)) 
                {
                    var produtoRelacionado = new ProdutoRelacionado(produtoRelacionadoId);
                    produtoRelacionado.AssociarAoProduto(produto.Id);
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
        _produtoRepository.Remover(id);
        await _produtoRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<Produto> ObterPorIdAsync(Guid id)
    {
        return await _produtoRepository.ObterProdutoPorId(id);
    }

    public async Task<Produto> ObterProdutoComImagensProdutos(Guid id)
    {
        return await _produtoRepository.ObterProdutoComImagensProdutos(id);
    }

    public async Task<IEnumerable<Produto>> ObterTodosAsync()
    {
        return await _produtoRepository.ObterTodosProdutos();
    }

    private bool ProdutoJaExiste(string nome)
    {
        if (_produtoRepository.BuscarAsync(c => c.Nome == nome).Result.Any())
        {
            Notify(ProdutoErrorMessages.ProdutoJaExiste);
            return true;
        }

        return false;
    }

    private bool ProdutoJaExiste(string nome, Guid id)
    {
        if (_produtoRepository.BuscarAsync(c => c.Nome == nome && c.Id != id).Result.Any())
        {
            Notify(ProdutoErrorMessages.ProdutoJaExiste);
            return true;
        }

        return false;
    }
    
    private async Task<Produto> ObterProduto(Guid id)
    {
        var produto = await _produtoRepository.ObterPorIdAsync(id);

        if (produto is null)
        {
            Notify(ProdutoErrorMessages.ProdutoNaoEncotrado);
            return null;
        }

        return produto;
    }

    public void Dispose()
    {
        _produtoRepository?.Dispose();
    }

}
