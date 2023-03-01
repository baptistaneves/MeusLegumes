namespace MeusLegumes.API.Controllers.V1;

[ApiVersion("1.0")]
[Route(ApiRoutes.BaseRoute)]
public class MotivosIsencaoIvaController : BaseController
{
    private readonly IMotivoIsencaoIvaAppService _motivoAppService;
    public MotivosIsencaoIvaController(INotifier notifier,
                                IMotivoIsencaoIvaAppService motivoAppService) : base(notifier)
    {
        _motivoAppService = motivoAppService;
    }

    [HttpGet(ApiRoutes.MotivoIsencaoIva.ObterMotivos)]
    public async Task<ActionResult> ObterMotivos()
    {
        return Ok(await _motivoAppService.ObterTodosAsync());
    }

    [HttpGet(ApiRoutes.MotivoIsencaoIva.ObterMotivoPorId)]
    [ValidateGuid("id")]
    public async Task<ActionResult> ObterMotivoPorId(Guid id)
    {
        var motivo = await _motivoAppService.ObterPorIdAsync(id);
        if (motivo is null) return NotFound();

        return Ok(motivo);
    }

    [HttpPost(ApiRoutes.MotivoIsencaoIva.NovoMotivo)]
    [ValidateModel]
    public async Task<ActionResult> NovoMotivo([FromBody] CriarMotivoIsencaoIva motivo, CancellationToken cancellationToken)
    {
        await _motivoAppService.Adicionar(motivo, cancellationToken);

        return Response(motivo);
    }

    [HttpPut(ApiRoutes.MotivoIsencaoIva.ActualizarMotivo)]
    [ValidateModel]
    public async Task<ActionResult> ActualizarMotivo([FromBody] ActualizarMotivoIsencaoIva motivo, CancellationToken cancellationToken)
    {
        await _motivoAppService.Actualizar(motivo, cancellationToken);

        return Response(motivo);
    }

    [HttpDelete(ApiRoutes.MotivoIsencaoIva.RemoverMotivo)]
    [ValidateGuid("id")]
    public async Task<ActionResult> RemoverMotivo(Guid id, CancellationToken cancellationToken)
    {
        var motivo = await _motivoAppService.ObterPorIdAsync(id);
        if (motivo is null) return NotFound();

        await _motivoAppService.Remover(id, cancellationToken);

        return Response();
    }
}
