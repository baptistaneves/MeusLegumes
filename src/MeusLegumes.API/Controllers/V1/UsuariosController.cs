namespace MeusLegumes.API.Controllers.V1;

[ApiVersion("1.0")]
[Route(ApiRoutes.BaseRoute)]
public class UsuariosController : BaseController
{
    private readonly IMediator _mediator;
    public UsuariosController(INotifier notifier, IMediator mediator) : base(notifier)
    {
        _mediator = mediator;
    }

    [HttpPost(ApiRoutes.Usuario.CriarUsuario)]
    public async Task<ActionResult> CriarUsuario([FromBody] CriarUsuario usuario, CancellationToken cancellationToken)
    {
        var command = new CriarUsuarioCommand(usuario.Nome, usuario.Email, usuario.Senha, usuario.Perfil);
        var result = await _mediator.Send(command, cancellationToken);

        if (result is null) return Response();

        return Response(result);
    }
}
