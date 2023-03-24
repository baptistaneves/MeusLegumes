namespace MeusLegumes.Infrastructure.Repositories.Pedidos;

internal class PedidoRepository : Repository<Pedido>, IPedidoRepository
{
    public PedidoRepository(ApplicationContext context) : base(context) { }

    public void AdicionarItem(PedidoItem pedidoItem)
    {
        _context.PedidoItensProduto.Add(pedidoItem);
    }

    public void AtualizarItem(PedidoItem pedidoItem)
    {
        _context.PedidoItensProduto.Update(pedidoItem);
    }

    public async Task<PedidoItem> ObterItemPorId(Guid id)
    {
        return await _context.PedidoItensProduto.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<PedidoItem> ObterItemPorPedido(Guid pedidoId, Guid produtoId)
    {
        return await _context.PedidoItensProduto.AsNoTracking()
            .FirstOrDefaultAsync(p => p.PedidoId == pedidoId && p.ProdutoId == produtoId);
    }

    public async Task<IEnumerable<Pedido>> ObterListaPorClienteId(Guid clienteId)
    {
        return await _context.Pedidos.AsNoTracking().Where(p => p.ClienteId == clienteId).ToListAsync();
    }

    public async Task<Pedido> ObterPedidoRascunhoPorClienteId(Guid clienteId)
    {
        var pedido = await _context.Pedidos.FirstOrDefaultAsync(p => p.ClienteId == clienteId && p.PedidoStatus == PedidoStatus.Rascunho);
        if (pedido == null) return null;

        await _context.Entry(pedido).Collection(i => i.PedidoItens).LoadAsync();

        return pedido;
    }

    public void RemoverItem(PedidoItem pedidoItem)
    {
        _context.PedidoItensProduto.Remove(pedidoItem);
    }
}
