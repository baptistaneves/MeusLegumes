using System.ComponentModel.DataAnnotations;

namespace MeusLegumes.Application.Contexts.Produtos.Contracts;

public class ActualizarProduto
{
    [Required(ErrorMessage = "O id do produto não é válido")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Informe a categoria")]
    public Guid CategoriaId { get; set; }

    [Required(ErrorMessage = "Informe a unidade")]
    public Guid UnidadeId { get; set; }

    [Required(ErrorMessage = "Informe o imposto")]
    public Guid ImpostoId { get; set; }

    [Required(ErrorMessage = "Informe o motivo")]
    public Guid MotivoId { get; set; }

    [Required(ErrorMessage = "Informe o nome")]
    public string Nome { get; set; }

    public string Descricao { get; set; }

    [Required(ErrorMessage = "Informe o preço unitário")]
    [Range(20, int.MaxValue, ErrorMessage = "O valor do produto deve ser maior que 20")]
    public decimal PrecoUnitario { get; set; }

    [Required(ErrorMessage = "Selecione uma imagem")]
    public string UrlImagemPrincipal { get; set; }

    public bool EmPromocao { get; set; }
    public decimal PrecoPromocional { get; set; }
    public bool Destaque { get; set; }
    public bool NovoLancamento { get; set; }
    public bool MaisVendido { get; set; }
    public bool MaisProcurado { get; set; }
    public bool EmEstoque { get; set; }
    public bool Activo { get; set; }
    public string Observacao { get; set; }

    public List<Guid> ProdutosRelacionados { get; set; } = new List<Guid>();
    public List<string>? ImagensOpcionaisUrls { get; set; } = new List<string>();

}

