namespace MeusLegumes.API.Controllers.V1;

[ApiVersion("1.0")]
[Route(ApiRoutes.BaseRoute)]
public class PacotesController : BaseController
{
    private readonly IPacoteAppService _pacoteAppService;
    private readonly IImageUploadService _imageUploadService;
    private readonly string _folderPath = "/wwwroot/imagens/pacotes";

    public PacotesController(INotifier notifier,
                             IPacoteAppService pacoteAppService,
                             IImageUploadService imageUploadService) : base(notifier)
    {
        _pacoteAppService = pacoteAppService;
        _imageUploadService = imageUploadService;
    }

    [HttpGet(ApiRoutes.Pacote.ObterPacotes)]
    public async Task<ActionResult> ObterPacotes()
    {
        return Ok(await _pacoteAppService.ObterTodosAsync());
    }

    [HttpGet(ApiRoutes.Pacote.ObterPacotePorId)]
    [ValidateGuid("id")]
    public async Task<ActionResult> ObterPacotePorId(Guid id)
    {
        var pacote = await _pacoteAppService.ObterPorIdAsync(id);
        if (pacote is null) return NotFound();

        return Ok(pacote);
    }

    [HttpPost(ApiRoutes.Pacote.NovoPacote)]
    [ValidateModel]
    public async Task<ActionResult> NovoPacote([FromBody] CriarPacote pacote, CancellationToken cancellationToken)
    {
        await _pacoteAppService.Adicionar(pacote, cancellationToken);

        if (!OperationIsValid()) _imageUploadService.DeleteImage(pacote.ImagemUrl, _folderPath);

        return Response(pacote);
    }

    [HttpPut(ApiRoutes.Pacote.ActualizarPacote)]
    [ValidateModel]
    public async Task<ActionResult> ActualizarPacote([FromBody] ActualizarPacote pacoteActualizado, CancellationToken cancellationToken)
    {
        var pacote =  await _pacoteAppService.Actualizar(pacoteActualizado, cancellationToken);

        if (OperationIsValid() && pacoteActualizado.ImagemUrl != pacote.UrlImagemPrincipal) 
            _imageUploadService.DeleteImage(pacote.UrlImagemPrincipal, _folderPath);

        return Response(pacoteActualizado);
    }

    [HttpDelete(ApiRoutes.Pacote.RemoverPacote)]
    [ValidateGuid("id")]
    public async Task<ActionResult> RemoverPacote(Guid id, CancellationToken cancellationToken)
    {
        var pacote = await _pacoteAppService.ObterPorIdAsync(id);
        if (pacote is null) return NotFound();

        await _pacoteAppService.Remover(id, cancellationToken);

        if(OperationIsValid()) _imageUploadService.DeleteImage(pacote.UrlImagemPrincipal, _folderPath);

        return Response();
    }

    [HttpPost(ApiRoutes.Pacote.UploadImagem)]
    public ActionResult UploadImagem(IFormFile file)
    {
        var urlImagem = _imageUploadService.UploadImage(file, _folderPath);

        return new JsonResult(urlImagem);
    }

}
