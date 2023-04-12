using MeusLegumes.Domain.Contexts.Pedidos.Entities;

namespace MeusLegumes.Application.Contexts.Pedidos.CommandHandlers;

public class AdicionarPedidoItemCommandHandler : IRequestHandler<AdicionarPedidoItemCommand, bool>
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly INotifier _notifier;

    public AdicionarPedidoItemCommandHandler(IPedidoRepository pedidoRepository, INotifier notifier)
    {
        _pedidoRepository = pedidoRepository;
        _notifier = notifier;
    }

    public async Task<bool> Handle(AdicionarPedidoItemCommand command, CancellationToken cancellationToken)
    {
        if (!ValidarComando(command)) return false;

        var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(command.ClienteId);
        var pedidoItem = new PedidoItem(command.ProdutoId, command.Nome, command.Quantidade, command.ValorUnitario);

        if(pedido is null)
        {
            pedido = Pedido.PedidoFactory.NovoPedidoRascunho(command.ClienteId);
            pedido.AdicionarItem(pedidoItem);

            _pedidoRepository.Adicionar(pedido);
        }
        else
        {
            var pedidoExistente = pedido.PedidoItemExistente(pedidoItem);
            pedido.AdicionarItem(pedidoItem);

            if(pedidoExistente)
            {
                _pedidoRepository.AtualizarItem(pedido.PedidoItens.FirstOrDefault(p => p.ProdutoId == pedidoItem.ProdutoId));
            }
            else
            {
                _pedidoRepository.AdicionarItem(pedidoItem);
            }
        }

        return await _pedidoRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    private bool ValidarComando(AdicionarPedidoItemCommand command)
    {
        if (command.IsValid()) return true;

        foreach (var error in command.ValidationResult.Errors)
        {
            _notifier.Handle(new Notification(error.ErrorMessage));
        }

        return false;
    }
}
