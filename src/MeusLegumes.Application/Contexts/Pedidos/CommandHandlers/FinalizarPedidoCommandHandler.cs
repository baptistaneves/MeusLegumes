namespace MeusLegumes.Application.Contexts.Pedidos.CommandHandlers;

public class FinalizarPedidoCommandHandler : IRequestHandler<FinalizarPedidoCommand, bool>
{

    private readonly IPedidoRepository _pedidoRepository;
    private readonly INotifier _notifier;
    public FinalizarPedidoCommandHandler(IPedidoRepository pedidoRepository, INotifier notifier)
    {
        _pedidoRepository = pedidoRepository;
        _notifier = notifier;
    }

    public async Task<bool> Handle(FinalizarPedidoCommand command, CancellationToken cancellationToken)
    {
        if(!ValidarComando(command)) return false;

        var pedido = await _pedidoRepository.ObterPorIdAsync(command.PedidoId);
        if (pedido is null)
        {
            _notifier.Handle(new Notification(PedidoErrorMessages.PedidoNaoEncotrado));
            return false;
        }

        pedido.FinalizarPedido();

        return await _pedidoRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    private bool ValidarComando(FinalizarPedidoCommand command)
    {
        if (command.IsValid()) return true;

        foreach (var error in command.ValidationResult.Errors)
        {
            _notifier.Handle(new Notification(error.ErrorMessage));
        }

        return false;
    }
}
