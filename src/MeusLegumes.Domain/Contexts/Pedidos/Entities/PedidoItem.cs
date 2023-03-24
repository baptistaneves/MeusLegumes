﻿namespace MeusLegumes.Domain.Contexts.Pedidos.Entities;

public class PedidoItem : Entity
{
    public Guid PedidoId { get; private set; }
    public Guid ProdutoId { get; private set; }
    public string ProdutoNome { get; private set; }
    public int Quantidade { get; private set; }
    public decimal ValorUnitario { get; private set; }

    public PedidoItem(Guid produtoId, string produtoNome, int quantidade, decimal valorUnitario)
    {
        ProdutoId = produtoId;
        ProdutoNome = produtoNome;
        Quantidade = quantidade;
        ValorUnitario = valorUnitario;
    }

    //EF Rel
    public Pedido Pedido { get; private set; }

    public PedidoItem() { }

    public void AssociarPedido(Guid pedidoId)
    {
        PedidoId = pedidoId;
    }

    public decimal CalcularValor()
    {
        return Quantidade * ValorUnitario;
    }

    internal void AdicionarUnidades(int unidades)
    {
        Quantidade += unidades;
    }

    internal void ActualizarUnidades(int unidades)
    {
        Quantidade = unidades;
    }
}
