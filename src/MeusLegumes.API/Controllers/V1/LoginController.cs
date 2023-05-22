namespace MeusLegumes.API.Controllers.V1;

[ApiVersion("1.0")]
[Route(ApiRoutes.BaseRoute)]
public class LoginController : BaseController
{
    private readonly IMediator _mediator;
    public LoginController(INotifier notifier, IMediator mediator) : base(notifier)
    {
        _mediator = mediator;
    }

    [HttpPost(ApiRoutes.Usuario.Login)]
    public async Task<ActionResult> Login([FromBody] Login login, CancellationToken cancellationToken)
    {
        var command = new LoginCommand(login.Email, login.Senha);
        var result = await _mediator.Send(command, cancellationToken);

        if (result is null) return Response();

        return Response(result);
    }
}
