namespace MeusLegumes.Application.Contexts.Identity.CommandHandlers;

public class AlterarSenhaCommandHandler : IRequestHandler<AlterarSenhaCommand, bool>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly INotifier _notifier;

    public AlterarSenhaCommandHandler(INotifier notifier,
                               IUsuarioRepository usuarioRepository)
    {
        _notifier = notifier;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<bool> Handle(AlterarSenhaCommand request, CancellationToken cancellationToken)
    {
        if (!ValidarComando(request)) return false;

        var usuario = await _usuarioRepository.ObterUsuarioPorId(request.Id);
        if (usuario is null)
        {
            _notifier.Handle(new Notification(IdentityErrorMessages.IdentityUserNotFound));
            return false;
        }

        var result = await AlterarSenha(request.Id, request.SenhaActual, request.NovaSenha);
        if(result is false) return false;

        return true;
    }

    private async Task<bool> AlterarSenha(Guid id, string senhaActual, string novaSenha)
    {
        var result = await _usuarioRepository.AlterarSenha(id, senhaActual, novaSenha);

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

    private bool ValidarComando(AlterarSenhaCommand command)
    {
        if (command.IsValid()) return true;

        foreach (var error in command.ValidationResult.Errors)
        {
            _notifier.Handle(new Notification(error.ErrorMessage));
        }

        return false;
    }
}