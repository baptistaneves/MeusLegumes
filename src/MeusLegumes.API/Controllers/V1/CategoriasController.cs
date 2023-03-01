namespace MeusLegumes.API.Controllers.V1;

[ApiVersion("1.0")]
[Route(ApiRoutes.BaseRoute)]
public class CategoriasController : BaseController
{
    private readonly ICategoriaAppService _categoriaAppService;
    public CategoriasController(INotifier notifier,
                                ICategoriaAppService categoriaAppService) : base(notifier)
    {
        _categoriaAppService = categoriaAppService;
    }

    [HttpGet(ApiRoutes.Categoria.ObterCategorias)]
    public async Task<ActionResult>ObterCategorias()
    {
        return Ok(await _categoriaAppService.ObterTodosAsync());
    }

    [HttpGet(ApiRoutes.Categoria.ObterCategoriaPorId)]
    [ValidateGuid("id")]
    public async Task<ActionResult> ObterCategoriaPorId(Guid id)
    {
        var categoria = await _categoriaAppService.ObterPorIdAsync(id);
        if (categoria is null) return NotFound();

        return Ok(categoria);
    }

    [HttpPost(ApiRoutes.Categoria.NovaCategoria)]
    [ValidateModel]
    public async Task<ActionResult> NovaCategoria([FromBody] CriarCategoria categoria, CancellationToken cancellationToken)
    {
        await _categoriaAppService.Adicionar(categoria, cancellationToken);

        return Response(categoria);
    }

    [HttpPut(ApiRoutes.Categoria.ActualizarCategoria)]
    [ValidateModel]
    public async Task<ActionResult> ActualizarCategoria([FromBody] ActualizarCategoria categoria, CancellationToken cancellationToken)
    {
        await _categoriaAppService.Actualizar(categoria, cancellationToken);

        return Response(categoria);
    }

    [HttpDelete(ApiRoutes.Categoria.RemoverCategoria)]
    [ValidateGuid("id")]
    public async Task<ActionResult> RemoverCategoria(Guid id, CancellationToken cancellationToken)
    {
        var categoria = await _categoriaAppService.ObterPorIdAsync(id);
        if (categoria is null) return NotFound();

        await _categoriaAppService.Remover(id, cancellationToken);

        return Response();
    }
}
