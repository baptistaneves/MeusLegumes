namespace MeusLegumes.API.Controllers.V1;

[ApiVersion("1.0")]
[Route(ApiRoutes.BaseRoute)]
public class ImpostosController : BaseController
{
    private readonly IImpostoAppService _impostoAppService;
    public ImpostosController(INotifier notifier,
                                IImpostoAppService impostoAppService) : base(notifier)
    {
        _impostoAppService = impostoAppService;
    }

    [HttpGet(ApiRoutes.Imposto.ObterImpostos)]
    public async Task<ActionResult> ObterImpostos()
    {
        return Ok(await _impostoAppService.ObterTodosAsync());
    }

    [HttpGet(ApiRoutes.Imposto.ObterImpostoPorId)]
    [ValidateGuid("id")]
    public async Task<ActionResult> ObterImpostoPorId(Guid id)
    {
        var imposto = await _impostoAppService.ObterPorIdAsync(id);
        if (imposto is null) return NotFound();

        return Ok(imposto);
    }

    [HttpPost(ApiRoutes.Imposto.NovoImposto)]
    [ValidateModel]
    public async Task<ActionResult> NovoImposto([FromBody] CriarImposto imposto, CancellationToken cancellationToken)
    {
        await _impostoAppService.Adicionar(imposto, cancellationToken);

        return Response(imposto);
    }

    [HttpPut(ApiRoutes.Imposto.ActualizarImposto)]
    [ValidateModel]
    public async Task<ActionResult> ActualizarImposto([FromBody] ActualizarImposto imposto, CancellationToken cancellationToken)
    {
        await _impostoAppService.Actualizar(imposto, cancellationToken);

        return Response(imposto);
    }

    [HttpDelete(ApiRoutes.Imposto.RemoverImposto)]
    [ValidateGuid("id")]
    public async Task<ActionResult> RemoverImposto(Guid id, CancellationToken cancellationToken)
    {
        var imposto = await _impostoAppService.ObterPorIdAsync(id);
        if (imposto is null) return NotFound();

        await _impostoAppService.Remover(id, cancellationToken);

        return Response();
    }
}
