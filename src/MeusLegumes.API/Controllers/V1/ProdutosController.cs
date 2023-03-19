namespace MeusLegumes.API.Controllers.V1;

[ApiVersion("1.0")]
[Route(ApiRoutes.BaseRoute)]
public class ProdutosController : BaseController
{
    private readonly IProdutoAppService _produtoAppService;
    private readonly IImageUploadService _imageUploadService;
    private readonly string _folderPath = "/wwwroot/imagens/produtos";
    public ProdutosController(INotifier notifier,
                              IProdutoAppService produtoAppService,
                              IWebHostEnvironment env,
                              IImageUploadService imageUploadService) : base(notifier)
    {
        _produtoAppService = produtoAppService;
        _imageUploadService = imageUploadService;
    }

    [HttpGet(ApiRoutes.Produto.ObterProdutos)]
    public async Task<ActionResult> ObterProdutos()
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
        await _produtoAppService.Adicionar(produto, cancellationToken);

        if (!OperationIsValid()) _imageUploadService.DeleteImage(produto.UrlImagemPrincipal, _folderPath);

        return Response(produto);
    }

    [HttpPut(ApiRoutes.Produto.ActualizarProduto)]
    [ValidateModel]
    public async Task<ActionResult> ActualizarProduto([FromBody] ActualizarProduto produtoActualizado, CancellationToken cancellationToken)
    {
        var produtoAntigo = await _produtoAppService.Actualizar(produtoActualizado, cancellationToken);

        if (OperationIsValid() && produtoActualizado.UrlImagemPrincipal != produtoAntigo.UrlImagemPrincipal)
            _imageUploadService.DeleteImage(produtoAntigo.UrlImagemPrincipal, _folderPath);

        return Response(produtoActualizado);
    }

    [HttpDelete(ApiRoutes.Produto.RemoverProduto)]
    [ValidateGuid("id")]
    public async Task<ActionResult> RemoverProduto(Guid id, CancellationToken cancellationToken)
    {
        var produto = await _produtoAppService.ObterProdutoComImagensProdutos(id);
        if (produto is null) return NotFound();

        await _produtoAppService.Remover(id, cancellationToken);

        if (OperationIsValid()) 
        {
            _imageUploadService.DeleteImage(produto.UrlImagemPrincipal, _folderPath);
            if (produto.ProdutosImagem.Any())
            {
                foreach (var produtoImage in produto.ProdutosImagem)
                {
                    _imageUploadService.DeleteImage(produtoImage.UrlImagem, _folderPath);
                }
            }
        }

        return Response();
    }

    [HttpPost(ApiRoutes.Produto.UploadImagem)]
    public ActionResult UploadImagem(IFormFile file)
    {
        var urlImagem = _imageUploadService.UploadImage(file, _folderPath);

        return new JsonResult(urlImagem);
    }

    [HttpPost(ApiRoutes.Produto.UploadMultiplasImagens)]
    public ActionResult UploadMultiplasImagens(List<IFormFile> files)
    {
        var fileNames = _imageUploadService.UploadMultipleImages(files, _folderPath);

        return new JsonResult(fileNames);
    }
}
