namespace MeusLegumes.Application.Contexts.Identity.CommandHandlers;

public class LoginCommandHandler : IRequestHandler<LoginCommand, IdentityResponse>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly INotifier _notifier;
    private readonly IJwtService _jwtService;

    public LoginCommandHandler(INotifier notifier,
                               IJwtService jwtService,
                               IUsuarioRepository usuarioRepository)
    {
        _notifier = notifier;
        _jwtService = jwtService;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<IdentityResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        if (!ValidarComando(request)) return null;

        var usuario = await _usuarioRepository.ObterUsuarioPorEmail(request.Email);
        if (usuario is null)
        {
            _notifier.Handle(new Notification( IdentityErrorMessages.IncorrectUserName));
            return null;
        }

        var siginResult = await _usuarioRepository.CheckPasswordAsync(usuario.Id, request.Password);

        if (siginResult.IsLockedOut)
        {
            _notifier.Handle(new Notification(IdentityErrorMessages.LockoutOnFailure));
            return null;
        }

        if (!siginResult.Succeeded)
        {
            _notifier.Handle(new Notification(IdentityErrorMessages.IncorrectPassword));
            return null;
        }

        return new IdentityResponse
        {
            Id = usuario.Id,
            Email = usuario.Email,
            Nome = usuario.UserName,
            Token = await _jwtService.GetJwtString(usuario)
        };
    }

    private bool ValidarComando(LoginCommand command)
    {
        if (command.IsValid()) return true;

        foreach (var error in command.ValidationResult.Errors)
        {
            _notifier.Handle(new Notification(error.ErrorMessage));
        }

        return false;
    }
}
