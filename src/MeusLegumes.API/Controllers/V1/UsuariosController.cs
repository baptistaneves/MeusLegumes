namespace MeusLegumes.API.Controllers.V1;

[ApiVersion("1.0")]
[Route(ApiRoutes.BaseRoute)]
public class UsuariosController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IUsuarioQueries _usuarioQueries;
    public UsuariosController(INotifier notifier, 
                              IMediator mediator, 
                              IUsuarioQueries usuarioQueries) : base(notifier)
    {
        _mediator = mediator;
        _usuarioQueries = usuarioQueries;
    }

    [HttpGet(ApiRoutes.Usuario.ObterUsuarios)]
    public async Task<ActionResult> ObterUsuarios()
    {
        return Ok(await _usuarioQueries.ObterUsuarios());
    }

    [HttpGet(ApiRoutes.Usuario.ObterUsuarioPorId)]
    [ValidateGuid("id")]
    public async Task<ActionResult> ObterUsuarioPorId(Guid id)
    {
        return Ok(await _usuarioQueries.ObterUsuarioPorId(id));
    }

    [HttpPost(ApiRoutes.Usuario.CriarUsuario)]
    public async Task<ActionResult> CriarUsuario([FromBody] CriarUsuario usuario, CancellationToken cancellationToken)
    {
        var command = new CriarUsuarioCommand(usuario.Nome, usuario.Email, usuario.Senha, usuario.Perfil);
        var result = await _mediator.Send(command, cancellationToken);

        if (result is null) return Response();

        return Response(result);
    }

    [HttpPost(ApiRoutes.Usuario.ActualizarUsuario)]
    public async Task<ActionResult> ActualizarUsuario([FromBody] ActualizarUsuario usuario, CancellationToken cancellationToken)
    {
        var command = new ActualizarUsuarioCommand(usuario.Id, usuario.Nome, usuario.Email, usuario.Perfil);
       
        await _mediator.Send(command, cancellationToken);

        return Response();
    }

    [HttpDelete(ApiRoutes.Usuario.RemoverUsuario)]
    [ValidateGuid("id")]
    public async Task<ActionResult> RemoverUsuario(Guid id, CancellationToken cancellationToken)
    {
        var command = new RemoverUsuarioCommand(id);
        
        await _mediator.Send(command, cancellationToken);

        return Response();
    }

    [HttpPost(ApiRoutes.Usuario.AlterarSenha)]
    public async Task<ActionResult> AlterarSenha(AlterarSenha alterarSenha, CancellationToken cancellationToken)
    {
        var command = new AlterarSenhaCommand(alterarSenha.Id, alterarSenha.SenhaActual, alterarSenha.NovaSenha);

        await _mediator.Send(command, cancellationToken);

        return Response();
    }

    
}
