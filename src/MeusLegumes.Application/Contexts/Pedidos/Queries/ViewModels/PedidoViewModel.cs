﻿namespace MeusLegumes.Application.Contexts.Pedidos.Queries.ViewModels;

public class PedidoViewModel
{
    public int Codigo { get; set; }
    public decimal ValorTotal { get; set; }
    public DateTime DataCadastro { get; set; }
    public int PedidoStatus { get; set; }
}
