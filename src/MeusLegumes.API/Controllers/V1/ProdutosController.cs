namespace MeusLegumes.API.Controllers.V1;

[ApiVersion("1.0")]
[Route(ApiRoutes.BaseRoute)]
public class ProdutosController : BaseController
{
    private readonly IProdutoAppService _produtoAppService;
    public ProdutosController(INotifier notifier,
                              IProdutoAppService produtoAppService) : base(notifier)
    {
        _produtoAppService = produtoAppService;
    }

    [HttpGet(ApiRoutes.Produto.ObterProdutos)]
    public async Task<ActionResult<ProdutoDto>> ObterProdutos()
    {
        return Ok(await _produtoAppService.ObterTodosAsync());
    }

    [HttpGet(ApiRoutes.Produto.ObterProdutoPorId)]
    [ValidateGuid("id")]
    public async Task<ActionResult> ObterProdutoPorId(Guid id)
    {
        var produto = await _produtoAppService.ObterPorIdAsync(id);
        if (produto is null) return NotFound();

        return Ok(produto);
    }

    [HttpPost(ApiRoutes.Produto.NovoProduto)]
    [ValidateModel]
    public async Task<ActionResult> NovoProduto([FromBody] CriarProduto produto, CancellationToken cancellationToken)
    {
        var imagemNome = Guid.NewGuid() + "_" + produto.UrlImagemPrincipal;
        if (!UploadArquivo(produto.ImagemUpload, imagemNome))
        {
            return Response(produto);
        }

        produto.UrlImagemPrincipal = imagemNome;

        await _produtoAppService.Adicionar(produto, cancellationToken);

        return Response(produto);
    }

    [HttpPut(ApiRoutes.Produto.ActualizarProduto)]
    [ValidateModel]
    public async Task<ActionResult> ActualizarProduto([FromBody] ActualizarProduto produtoAct, CancellationToken cancellationToken)
    {
        var produto = await _produtoAppService.ObterPorIdAsync(produtoAct.Id);
        if (produto is null)
        {
            Notify("Não existe nenhum produto com o Id informado");
            return Response(produtoAct);
        }

        if (produtoAct.ImagemUpload != null)
        {
            var imagemNome = Guid.NewGuid() + "_" + produtoAct.UrlImagemPrincipal;
            if (!UploadArquivo(produtoAct.ImagemUpload, imagemNome))
            {
                return Response(produtoAct);
            }

            produtoAct.UrlImagemPrincipal = imagemNome;
        }

        await _produtoAppService.Actualizar(produtoAct, cancellationToken);

        if (OperationIsValid()) DeleteImage(produto.UrlImagemPrincipal);

        return Response(produtoAct);
    }

    [HttpDelete(ApiRoutes.Produto.RemoverProduto)]
    [ValidateGuid("id")]
    public async Task<ActionResult> RemoverProduto(Guid id, CancellationToken cancellationToken)
    {
        var produto = await _produtoAppService.ObterProdutoComImagensProdutos(id);
        if (produto is null) return NotFound();

        await _produtoAppService.Remover(id, cancellationToken);

        if (OperationIsValid()) DeleteImage(produto.UrlImagemPrincipal);

        return Response();
    }

    private bool UploadArquivo(string arquivo, string imgNome)
    {
        if (string.IsNullOrEmpty(arquivo))
        {
            Notify("Forneça uma imagem para este produto!");
            return false;
        }

        var imageDataByteArray = Convert.FromBase64String(arquivo);

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens/produtos", imgNome);

        if (System.IO.File.Exists(filePath))
        {
            Notify("Já existe um arquivo com este nome!");
            return false;
        }

        System.IO.File.WriteAllBytes(filePath, imageDataByteArray);

        return true;
    }

    private void DeleteImage(string imageFileName)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/produtos/" + imageFileName);

        if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
    }
}
