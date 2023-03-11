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
        var imgPrefixo = Guid.NewGuid() + "_";

        if (!await Upload(pacote.Imagem, imgPrefixo))
        {
            return Response(pacote);
        }

        var fileName = imgPrefixo + pacote.Imagem.FileName;

        pacote.AdicionarImagemUrl(fileName);

        await _pacoteAppService.Adicionar(pacote, cancellationToken);

        return Response(pacote);
    }

    [HttpPut(ApiRoutes.Pacote.ActualizarPacote)]
    [ValidateModel]
    public async Task<ActionResult> ActualizarPacote([FromBody] ActualizarPacote pacote, CancellationToken cancellationToken)
    {
        await _pacoteAppService.Actualizar(pacote, cancellationToken);

        return Response(pacote);
    }

    [HttpDelete(ApiRoutes.Pacote.RemoverPacote)]
    [ValidateGuid("id")]
    public async Task<ActionResult> RemoverPacote(Guid id, CancellationToken cancellationToken)
    {
        var pacote = await _pacoteAppService.ObterPorIdAsync(id);
        if (pacote is null) return NotFound();

        await _pacoteAppService.Remover(id, cancellationToken);

        return Response();
    }

    [NonAction]
    private async Task<bool> Upload(IFormFile arquivo, string imgPrefixo)
    {
        if (arquivo == null || arquivo.Length == 0)
        {
            Notify("Forneça uma imagem para este pacote!");
            return false;
        }

        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens/pacotes", imgPrefixo + arquivo.FileName);

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
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens/pacotes/" + imageFileName);

        if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
    }
}
