namespace MeusLegumes.Domain.Contexts.Pedidos.Entities;

public class Pedido : Entity
{
    public int Codigo { get; private set; }
    public Guid ClienteId { get; private set; }
    public decimal ValorTotal { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public PedidoStatus PedidoStatus { get; private set; }

    private readonly List<PedidoItem> _peditoItens = new List<PedidoItem>();
    public IReadOnlyCollection<PedidoItem> PedidoItens => _peditoItens;


    public Pedido(Guid clienteId, decimal valorTotal, DateTime dataCadastro)
    {
        ClienteId = clienteId;
        ValorTotal = valorTotal;
        DataCadastro = dataCadastro;
        _peditoItens = new List<PedidoItem>();
    }

    //EF Rel
    public Pedido()
    {
        _peditoItens = new List<PedidoItem>();
    }

    public void CalcularValorPedido()
    {
        ValorTotal = _peditoItens.Sum(p => p.CalcularValor());
    }

    public bool PedidoItemExistente(PedidoItem item)
    {
        return _peditoItens.Any(p => p.ProdutoId == item.ProdutoId);
    }

    public void AdicionarItem(PedidoItem item)
    {
        item.AssociarPedido(Id);

        if (PedidoItemExistente(item))
        {
            var itemExistente = _peditoItens.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);
            itemExistente.AdicionarUnidades(item.Quantidade);
            item = itemExistente;

            _peditoItens.Remove(itemExistente);
        }

        item.CalcularValor();
        _peditoItens.Add(item);

        CalcularValorPedido();
    }

    public void RemoverItem(PedidoItem item)
    {
        var itemExistente = _peditoItens.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);
        if (itemExistente is null) throw new DomainException("o item não pertence ao produto");

        _peditoItens.Remove(item);

        CalcularValorPedido();
    }

    public void ActualizarItem(PedidoItem item)
    {
        item.AssociarPedido(Id);

        var itemExistente = _peditoItens.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);
        if (itemExistente is null) throw new DomainException("o item não pertence ao produto");

        _peditoItens.Remove(itemExistente);
        _peditoItens.Add(item);

        CalcularValorPedido();
    }

    public void ActualizarUnidades(PedidoItem item, int unidades)
    {
        item.ActualizarUnidades(unidades);
        ActualizarItem(item);
    }

    public void TornarRascunho()
    {
        PedidoStatus = PedidoStatus.Rascunho;
    }

    public void IniciarPedido()
    {
        PedidoStatus = PedidoStatus.Iniciado;
    }

    public void FinalizarPedido()
    {
        PedidoStatus = PedidoStatus.Pago;
    }

    public void CancelarPedido()
    {
        PedidoStatus = PedidoStatus.Cancelado;
    }

    public static class PedidoFactory
    {
        public static Pedido NovoPedidoRascunho(Guid clienteId)
        {
            var pedido = new Pedido
            {
                ClienteId = clienteId,
            };

            pedido.TornarRascunho();
            return pedido;
        }
    }
}
