namespace MeusLegumes.Application.Contexts.Identity.CommandHandlers;

public class ActualizarUsuarioCommandHandler : IRequestHandler<ActualizarUsuarioCommand, bool>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly INotifier _notifier;


    public ActualizarUsuarioCommandHandler(INotifier notifier,
                                      IUsuarioRepository usuarioRepository)
    {
        _notifier = notifier;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<bool> Handle(ActualizarUsuarioCommand request, CancellationToken cancellationToken)
    {
        if (!ValidarComando(request)) return false;

        var usuario = await ObterUsuario(request.Id);
        if (usuario == null) return false;

        var usuarioAct = new Usuario(request.Name, request.Email);
        usuarioAct.Id = request.Id;

        if (!await ActualizarUsuario(usuarioAct)) return false;

        if (!string.IsNullOrEmpty(usuario.Perfil) && usuario.Perfil != request.Perfil)
        {
            if (!await RemoverUsuarioDoPerfil(usuarioAct.Id, usuario.Perfil)) return false; 

            if (!await AdicionarUsuarioAoPerfil(usuarioAct.Id, request.Perfil)) return false;
        }

        return true;
    }

    private async Task<bool> ActualizarUsuario(Usuario usuario)
    {
        var result = await _usuarioRepository.Actualizar(usuario);

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

    private async Task<bool> AdicionarUsuarioAoPerfil(Guid id, string perlfil)
    {
        var result = await _usuarioRepository.AdicionarAoPerfil(id, perlfil);

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

    private async Task<bool> RemoverUsuarioDoPerfil(Guid id, string perlfil)
    {
        var result = await _usuarioRepository.RemoverUsuarioDoPerfil(id, perlfil);

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

    private async Task<UsuarioDto> ObterUsuario(Guid id)
    {
        var usuario = await _usuarioRepository.ObterUsuarioPorId(id);

        if(usuario is null)
        {
            _notifier.Handle(new Notification(IdentityErrorMessages.IdentityUserNotFound));

            return null;
        }

        return usuario;
    }

    private bool ValidarComando(ActualizarUsuarioCommand command)
    {
        if (command.IsValid()) return true;

        foreach (var error in command.ValidationResult.Errors)
        {
            _notifier.Handle(new Notification(error.ErrorMessage));
        }

        return false;
    }
}