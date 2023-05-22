namespace MeusLegumes.API.Controllers.V1;

[ApiVersion("1.0")]
[Route(ApiRoutes.BaseRoute)]
public class PacotesController : BaseController
{
    private readonly IPacoteAppService _pacoteAppService;

    public PacotesController(INotifier notifier,
                             IPacoteAppService pacoteAppService) : base(notifier)
    {
        _pacoteAppService = pacoteAppService;
    }

    [HttpGet(ApiRoutes.Pacote.ObterPacotes)]
    public async Task<ActionResult<PacoteDto>> ObterPacotes()
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
        var imagemNome = Guid.NewGuid() + "_" + pacote.UrlImagemPrincipal;
        if (!UploadArquivo(pacote.ImagemUpload, imagemNome))
        {
            return Response(pacote);
        }

        pacote.UrlImagemPrincipal = imagemNome;

        await _pacoteAppService.Adicionar(pacote, cancellationToken);

        return Response(pacote);
    }

    [HttpPut(ApiRoutes.Pacote.ActualizarPacote)]
    [ValidateModel]
    public async Task<ActionResult> ActualizarPacote([FromBody] ActualizarPacote pacoteAct, CancellationToken cancellationToken)
    {
        var pacote = await _pacoteAppService.ObterPorIdAsync(pacoteAct.Id);
        if (pacote is null)
        {
            Notify("Não existe nenhum pacote com o Id informado");
            return Response(pacoteAct);
        }

        if (pacoteAct.ImagemUpload != null)
        {
            var imagemNome = Guid.NewGuid() + "_" + pacoteAct.UrlImagemPrincipal;
            if (!UploadArquivo(pacoteAct.ImagemUpload, imagemNome))
            {
                return Response(pacoteAct);
            }

            pacoteAct.UrlImagemPrincipal = imagemNome;
        }

        await _pacoteAppService.Actualizar(pacoteAct, cancellationToken);

        if (OperationIsValid()) DeleteImage(pacote.UrlImagemPrincipal);

        return Response(pacoteAct);
    }

    [HttpDelete(ApiRoutes.Pacote.RemoverPacote)]
    [ValidateGuid("id")]
    public async Task<ActionResult> RemoverPacote(Guid id, CancellationToken cancellationToken)
    {
        var pacote = await _pacoteAppService.ObterPorIdAsync(id);
        if (pacote is null) return NotFound();

        await _pacoteAppService.Remover(id, cancellationToken);

        if (OperationIsValid()) DeleteImage(pacote.UrlImagemPrincipal);

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

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens/pacotes", imgNome);

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
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/pacotes/" + imageFileName);

        if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
    }
}
