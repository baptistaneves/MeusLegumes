namespace MeusLegumes.Application.Contexts.Pacotes.Contracts;

public class ActualizarPacoteProduto
{
    public Guid Id { get; set; }
    public Guid ProdutoId { get; set; }
    public Guid UnidadeId { get; set; }
    public int Quantidade { get; set; }
}