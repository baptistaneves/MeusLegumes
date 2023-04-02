namespace MeusLegumes.Application.Contexts.Identity.CommandHandlers;

public class CriarUsuarioCommandHandler : IRequestHandler<CriarUsuarioCommand, IdentityResponse>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly INotifier _notifier;
    private readonly IJwtService _jwtService;


    public CriarUsuarioCommandHandler(INotifier notifier,
                                      IJwtService jwtService,
                                      IUsuarioRepository usuarioRepository)
    {
        _notifier = notifier;
        _jwtService = jwtService;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<IdentityResponse> Handle(CriarUsuarioCommand request, CancellationToken cancellationToken)
    {
        if (!ValidarComando(request)) return null;

        var usuario = new Usuario(request.Name, request.Email);

        if (!await CriarUsuario(usuario, request.Password)) return null;

        if (!await AdicionarUsuarioAoPerfil(usuario, request.Perfil)) return null;

        return new IdentityResponse
        {
            Id = usuario.Id,
            Email = usuario.Email,
            Nome = usuario.UserName,
            Token = await _jwtService.GetJwtString(usuario)
        };
    }

    private async Task<bool> CriarUsuario(Usuario usuario, string password)
    {
        var result = await _usuarioRepository.Adicionar(usuario, password);

        if (!result.Success)
        {
            foreach (var error in result.Errors)
            {
                _notifier.Handle(new Notification(error.Mensagem));
                return false;
            }

        }

        return true;
    }

    private async Task<bool> AdicionarUsuarioAoPerfil(Usuario usuario, string perlfil)
    {
        var result = await _usuarioRepository.AdicionarAoPerfil(usuario.Id, perlfil);

        if (!result.Success)
        {
            foreach (var error in result.Errors)
            {
                
                _notifier.Handle(new Notification(error.Mensagem));
                return false;
            }
        }
        return true;
    }

    private bool ValidarComando(CriarUsuarioCommand command)
    {
        if (command.IsValid()) return true;

        foreach (var error in command.ValidationResult.Errors)
        {
            _notifier.Handle(new Notification(error.ErrorMessage));
        }

        return false;
    }
}
