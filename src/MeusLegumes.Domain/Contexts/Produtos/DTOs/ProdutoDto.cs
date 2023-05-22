namespace MeusLegumes.Domain.Contexts.Produtos.DTOs;

public class ProdutoDto
{
    public Guid Id { get; set; }
    public Guid? CategoriaId { get; set; }
    public Guid? UnidadeId { get; set; }
    public Guid? ImpostoId { get; set; }
    public Guid? MotivoId { get; set; }
    public string CategoriaDescricao { get; set; }
    public string UnidadeDescricao { get; set; }
    public string ImpostoDescricao { get; set; }
    public string MotivoDescricao { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal PrecoUnitario { get; set; }
    public string UrlImagemPrincipal { get; set; }
    public bool EmPromocao { get; set; }
    public decimal? PrecoPromocional { get; set; }
    public bool Destaque { get; set; }
    public bool NovoLancamento { get; set; }
    public bool MaisVendido { get; set; }
    public bool MaisProcurado { get; set; }
    public bool EmEstoque { get; set; }
    public bool Activo { get; set; }
    public string Observacao { get; set; }
}

