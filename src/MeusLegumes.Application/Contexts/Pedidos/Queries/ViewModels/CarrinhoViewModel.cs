namespace MeusLegumes.Application.Contexts.Pedidos.Queries.ViewModels;

public class CarrinhoViewModel
{
    public Guid PedidoId { get; set; }
    public Guid ClienteId { get; set; }
    public decimal ValorTotal { get; set; }

    public List<CarrinhoItemViewModel> Items { get; set; } = new List<CarrinhoItemViewModel>();
}
