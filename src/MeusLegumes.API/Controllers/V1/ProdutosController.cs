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
    public async Task<ActionResult> NovoProduto([FromForm] CriarProduto produto, CancellationToken cancellationToken)
    {
        var imgPrefixo = Guid.NewGuid() + "_";

        if (!await Upload(produto.Imagem, imgPrefixo))
        {
            return Response(produto);
        }

        var fileName = imgPrefixo + produto.Imagem.FileName;

        produto.AdicionarImagemUrl(fileName);

        await _produtoAppService.Adicionar(produto, cancellationToken);

        return Response(produto);
    }

    [HttpPut(ApiRoutes.Produto.ActualizarProduto)]
    [ValidateModel]
    public async Task<ActionResult> ActualizarProduto([FromBody] ActualizarProduto produto, CancellationToken cancellationToken)
    {
        await _produtoAppService.Actualizar(produto, cancellationToken);

        return Response(produto);
    }

    [HttpDelete(ApiRoutes.Produto.RemoverProduto)]
    [ValidateGuid("id")]
    public async Task<ActionResult> RemoverProduto(Guid id, CancellationToken cancellationToken)
    {
        var produto = await _produtoAppService.ObterPorIdAsync(id);
        if (produto is null) return NotFound();

        await _produtoAppService.Remover(id, cancellationToken);

        return Response();
    }

    [NonAction]
    private async Task<bool> Upload(IFormFile arquivo, string imgPrefixo)
    {
        if (arquivo == null || arquivo.Length == 0)
        {
            Notify("Forneça uma imagem para este produto!");
            return false;
        }

        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens/produtos", imgPrefixo + arquivo.FileName);

        if (System.IO.File.Exists(path))
        {
            Notify("Já existe um arquivo come este nome!");
            return false;
        }

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await arquivo.CopyToAsync(stream);
        }

        return true;
    }

    [NonAction]
    public void DeleteImage(string imageFileName)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens/produtos/" + imageFileName);

        if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
    }
}
