namespace MeusLegumes.API.Controllers.V1;

[ApiVersion("1.0")]
[Route(ApiRoutes.BaseRoute)]
public class ClientesController : BaseController
{
    private readonly IClienteAppService _clienteAppService;
    private readonly IMediator _mediator;
    public ClientesController(INotifier notifier,
                                IClienteAppService clienteAppService,
                                IMediator mediator) : base(notifier)
    {
        _clienteAppService = clienteAppService;
        _mediator = mediator;
    }

    [HttpGet(ApiRoutes.Cliente.ObterClientes)]
    public async Task<ActionResult> ObterClientes()
    {
        return Ok(await _clienteAppService.ObterTodosAsync());
    }

    [HttpGet(ApiRoutes.Cliente.ObterClientePorId)]
    [ValidateGuid("id")]
    public async Task<ActionResult> ObterClientePorId(Guid id)
    {
        var cliente = await _clienteAppService.ObterPorIdAsync(id);
        if (cliente is null) return NotFound();

        return Ok(cliente);
    }

    [HttpPost(ApiRoutes.Cliente.NovoCliente)]
    [ValidateModel]
    public async Task<ActionResult> NovoCliente([FromBody] CriarCliente cliente, CancellationToken cancellationToken)
    {
        var command = new CriarUsuarioCommand(cliente.Nome, cliente.Email, cliente.Senha, PerfilUsuario.CLIENTE);
        var identity = await _mediator.Send(command, cancellationToken);

        if (identity is null) return Response();

        await _clienteAppService.Adicionar(cliente, identity.Id, cancellationToken);

        return Response(cliente);
    }

    [HttpPut(ApiRoutes.Cliente.ActualizarCliente)]
    [ValidateModel]
    public async Task<ActionResult> ActualizarCliente([FromBody] ActualizarCliente cliente, CancellationToken cancellationToken)
    {
        await _clienteAppService.Actualizar(cliente, cancellationToken);

        return Response(cliente);
    }

    [HttpDelete(ApiRoutes.Cliente.RemoverCliente)]
    [ValidateGuid("id")]
    public async Task<ActionResult> RemoverCliente(Guid id, CancellationToken cancellationToken)
    {
        var cliente = await _clienteAppService.ObterPorIdAsync(id);
        if (cliente is null) return NotFound();

        await _clienteAppService.Remover(id, cancellationToken);

        return Response();
    }
}
