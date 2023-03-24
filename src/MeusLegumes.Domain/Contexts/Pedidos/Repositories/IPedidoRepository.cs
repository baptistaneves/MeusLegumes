namespace MeusLegumes.Domain.Contexts.Pedidos.Repositories;

public interface IPedidoRepository : IRepository<Pedido>
{
    Task<IEnumerable<Pedido>> ObterListaPorClienteId(Guid clienteId);
    Task<Pedido> ObterPedidoRascunhoPorClienteId(Guid clienteId);

    Task<PedidoItem> ObterItemPorId(Guid id);
    Task<PedidoItem> ObterItemPorPedido(Guid pedidoId, Guid produtoId);
    void AdicionarItem(PedidoItem pedidoItem);
    void AtualizarItem(PedidoItem pedidoItem);
    void RemoverItem(PedidoItem pedidoItem);

}
