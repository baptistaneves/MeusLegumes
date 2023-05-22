namespace MeusLegumes.Domain.Contexts.Produtos.DTOs;

public class PacoteDto
{
    public Guid Id { get; set; }
    public Guid? UnidadeId { get; set; }
    public string UnidadeDescricao { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal PrecoUnitario { get; set; }
    public string UrlImagemPrincipal { get; set; }
    public bool EmPromocao { get; set; }
    public decimal? PrecoPromocional { get; set; }
    public bool Activo { get; set; }
}

