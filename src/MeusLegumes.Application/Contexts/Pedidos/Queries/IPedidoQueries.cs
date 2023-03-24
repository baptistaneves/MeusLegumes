namespace MeusLegumes.Application.Contexts.Pedidos.Queries;

public interface IPedidoQueries
{
    Task<CarrinhoViewModel> ObterCarrinhoCliente(Guid clienteId);
    Task<IEnumerable<PedidoViewModel>> ObterPedidosCliente(Guid clienteId);
}
