namespace MeusLegumes.Application.Contexts.Pedidos.CommandHandlers;

public class IniciarPedidoCommandHandler : IRequestHandler<IniciarPedidoCommand, bool>
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly INotifier _notifier;
    public IniciarPedidoCommandHandler(IPedidoRepository pedidoRepository, INotifier notifier)
    {
        _pedidoRepository = pedidoRepository;
        _notifier = notifier;
    }

    public async Task<bool> Handle(IniciarPedidoCommand command, CancellationToken cancellationToken)
    {
        if(!ValidarComando(command)) return false;

        var pedido = await _pedidoRepository.ObterPorIdAsync(command.PedidoId);
        if (pedido is null)
        {
            _notifier.Handle(new Notification(PedidoErrorMessages.PedidoNaoEncotrado));
            return false;
        }
        pedido.IniciarPedido();

        _pedidoRepository.Actualizar(pedido);

        return await _pedidoRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    private bool ValidarComando(IniciarPedidoCommand command)
    {
        if (command.IsValid()) return true;

        foreach (var error in command.ValidationResult.Errors)
        {
            _notifier.Handle(new Notification(error.ErrorMessage));
        }

        return false;
    }
}
