namespace MeusLegumes.API.Controllers.V1;

[ApiVersion("1.0")]
[Route(ApiRoutes.BaseRoute)]
public class ProvinciasController : BaseController
{
    private readonly IProvinciaAppService _provinciaAppService;
    public ProvinciasController(INotifier notifier,
                                IProvinciaAppService provinciaAppService) : base(notifier)
    {
        _provinciaAppService = provinciaAppService;
    }

    [HttpGet(ApiRoutes.Provincia.ObterProvincias)]
    public async Task<ActionResult> ObterProvincias()
    {
        return Ok(await _provinciaAppService.ObterTodosAsync());
    }

    [HttpGet(ApiRoutes.Provincia.ObterProvinciaPorId)]
    [ValidateGuid("id")]
    public async Task<ActionResult> ObterProvinciaPorId(Guid id)
    {
        var provincia = await _provinciaAppService.ObterPorIdAsync(id);
        if (provincia is null) return NotFound();

        return Ok(provincia);
    }

    [HttpPost(ApiRoutes.Provincia.NovaProvincia)]
    [ValidateModel]
    public async Task<ActionResult> NovaProvincia([FromBody] CriarProvincia provincia, CancellationToken cancellationToken)
    {
        await _provinciaAppService.Adicionar(provincia, cancellationToken);

        return Response(provincia);
    }

    [HttpPut(ApiRoutes.Provincia.ActualizarProvincia)]
    [ValidateModel]
    public async Task<ActionResult> ActualizarProvincia([FromBody] ActualizarProvincia provincia, CancellationToken cancellationToken)
    {
        await _provinciaAppService.Actualizar(provincia, cancellationToken);

        return Response(provincia);
    }

    [HttpDelete(ApiRoutes.Provincia.RemoverProvincia)]
    [ValidateGuid("id")]
    public async Task<ActionResult> RemoverProvincia(Guid id, CancellationToken cancellationToken)
    {
        var provincia = await _provinciaAppService.ObterPorIdAsync(id);
        if (provincia is null) return NotFound();

        await _provinciaAppService.Remover(id, cancellationToken);

        return Response();
    }

    [HttpGet(ApiRoutes.Provincia.ObterMunicipios)]
    public async Task<ActionResult> ObterMunicipios()
    {
        return Ok(await _provinciaAppService.ObterMunicipiosAsync());
    }

    [HttpGet(ApiRoutes.Provincia.ObterMunicipioPorId)]
    [ValidateGuid("id")]
    public async Task<ActionResult> ObterMunicipioPorId(Guid id)
    {
        var municipio = await _provinciaAppService.ObterMunicipioPorIdAsync(id);
        if (municipio is null) return NotFound();

        return Ok(municipio);
    }

    [HttpPost(ApiRoutes.Provincia.NovoMunicipio)]
    [ValidateModel]
    public async Task<ActionResult> NovoMunicipio([FromBody] CriarMunicipio municipio, CancellationToken cancellationToken)
    {
        await _provinciaAppService.AdicionarMunicipio(municipio, cancellationToken);

        return Response(municipio);
    }

    [HttpPut(ApiRoutes.Provincia.ActualizarMunicipio)]
    [ValidateModel]
    public async Task<ActionResult> ActualizarMunicipio([FromBody] ActualizarMunicipio municipio, CancellationToken cancellationToken)
    {
        await _provinciaAppService.ActualizarMunicipio(municipio, cancellationToken);

        return Response(municipio);
    }

    [HttpDelete(ApiRoutes.Provincia.RemoverMunicipio)]
    [ValidateGuid("id")]
    public async Task<ActionResult> RemoverMunicipio(Guid id, CancellationToken cancellationToken)
    {
        var municipio = await _provinciaAppService.ObterMunicipioPorIdAsync(id);
        if (municipio is null) return NotFound();

        await _provinciaAppService.RemoverMunicipio(municipio, cancellationToken);

        return Response();
    }
}
