using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeusLegumes.Application.Contexts.Produtos.Contracts;

public class CriarProduto
{
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

    public string UrlImagemPrincipal { get; private set; }

    public IFormFile Imagem { get; set; }

    public bool EmPromocao { get; set; }
    public decimal PrecoPromocional { get; set; }
    public bool Destaque { get; set; }
    public bool NovoLancamento { get; set; }
    public bool MaisVendido { get; set; }
    public bool MaisProcurado { get; set; }
    public bool EmEstoque { get; set; }
    public bool Activo { get; set; }
    public string Observacao { get; set; }

    public IEnumerable<CriarProdutoRelacionado> ProdutosRelacionados { get; set; }
    public IEnumerable<CriarProdutoImagem> ProdutoImagens { get; set; }

    public void AdicionarImagemUrl(string imageUrl) => UrlImagemPrincipal = imageUrl;
}