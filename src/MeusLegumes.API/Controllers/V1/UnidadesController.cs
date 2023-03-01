namespace MeusLegumes.API.Controllers.V1;

[ApiVersion("1.0")]
[Route(ApiRoutes.BaseRoute)]
public class UnidadesController : BaseController
{
    private readonly IUnidadeAppService _unidadeAppService;
    public UnidadesController(INotifier notifier,
                              IUnidadeAppService unidadeAppService) : base(notifier)
    {
        _unidadeAppService = unidadeAppService;
    }

    [HttpGet(ApiRoutes.Unidade.ObterUnidades)]
    public async Task<ActionResult> ObterUnidades()
    {
        return Ok(await _unidadeAppService.ObterTodosAsync());
    }

    [HttpGet(ApiRoutes.Unidade.ObterUnidadePorId)]
    [ValidateGuid("id")]
    public async Task<ActionResult> ObterUnidadePorId(Guid id)
    {
        var unidade = await _unidadeAppService.ObterPorIdAsync(id);
        if (unidade is null) return NotFound();

        return Ok(unidade);
    }

    [HttpPost(ApiRoutes.Unidade.NovaUnidade)]
    [ValidateModel]
    public async Task<ActionResult> NovaUnidade([FromBody] CriarUnidade unidade, CancellationToken cancellationToken)
    {
        await _unidadeAppService.Adicionar(unidade, cancellationToken);

        return Response(unidade);
    }

    [HttpPut(ApiRoutes.Unidade.ActualizarUnidade)]
    [ValidateModel]
    public async Task<ActionResult> ActualizarCategoria([FromBody] ActualizarUnidade unidade, CancellationToken cancellationToken)
    {
        await _unidadeAppService.Actualizar(unidade, cancellationToken);

        return Response(unidade);
    }

    [HttpDelete(ApiRoutes.Unidade.RemoverUnidade)]
    [ValidateGuid("id")]
    public async Task<ActionResult> RemoverUnidade(Guid id, CancellationToken cancellationToken)
    {
        var unidade = await _unidadeAppService.ObterPorIdAsync(id);
        if (unidade is null) return NotFound();

        await _unidadeAppService.Remover(id, cancellationToken);

        return Response();
    }
}
