namespace MeusLegumes.Application.Contexts.Pedidos.CommandHandlers;

public class RemoverPedidoItemCommandHandler : IRequestHandler<RemoverPedidoItemCommand, bool>
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly INotifier _notifier;
    public RemoverPedidoItemCommandHandler(IPedidoRepository pedidoRepository, INotifier notifier)
    {
        _pedidoRepository = pedidoRepository;
        _notifier = notifier;
    }

    public async Task<bool> Handle(RemoverPedidoItemCommand command, CancellationToken cancellationToken)
    {
        if(!ValidarComando(command)) return false;

        var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(command.ClienteId);
        if (pedido is null)
        {
            _notifier.Handle(new Notification(PedidoErrorMessages.PedidoNaoEncotrado));
            return false;
        }

        var pedidoItem = await _pedidoRepository.ObterItemPorPedido(pedido.Id, command.ProdutoId);
        if (pedidoItem != null && !pedido.PedidoItemExistente(pedidoItem))
        {
            _notifier.Handle(new Notification(PedidoErrorMessages.PedidoItemNaoEncotrado));
            return false;
        }

        pedido.RemoverItem(pedidoItem);

        _pedidoRepository.RemoverItem(pedidoItem);
        _pedidoRepository.Actualizar(pedido);

        return await _pedidoRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    private bool ValidarComando(RemoverPedidoItemCommand command)
    {
        if (command.IsValid()) return true;

        foreach (var error in command.ValidationResult.Errors)
        {
            _notifier.Handle(new Notification(error.ErrorMessage));
        }

        return false;
    }
}
