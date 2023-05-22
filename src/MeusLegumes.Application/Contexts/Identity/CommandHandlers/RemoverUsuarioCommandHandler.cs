namespace MeusLegumes.Application.Contexts.Identity.CommandHandlers;

public class RemoverUsuarioCommandHandler : IRequestHandler<RemoverUsuarioCommand, bool>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly INotifier _notifier;

    public RemoverUsuarioCommandHandler(IUsuarioRepository usuarioRepository, INotifier notifier)
    {
        _usuarioRepository = usuarioRepository;
        _notifier = notifier;
    }

    public async Task<bool> Handle(RemoverUsuarioCommand request, CancellationToken cancellationToken)
    {
        var result = await _usuarioRepository.Remover(request.Id);

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
}
