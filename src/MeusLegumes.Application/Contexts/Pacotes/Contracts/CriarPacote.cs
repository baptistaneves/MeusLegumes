using System.ComponentModel.DataAnnotations;

namespace MeusLegumes.Application.Contexts.Pacotes.Contracts;

public class CriarPacote
{
    [Required(ErrorMessage = "Infome a unidade do produto")]
    public Guid UnidadeId { get; set; }

    [Required(ErrorMessage = "Informe o nome")]
    public string Nome { get; set; }

    public string Descricao { get; set; }

    [Required(ErrorMessage = "Informe o preço unitário")]
    [Range(20, int.MaxValue, ErrorMessage = "O valor do pacote deve ser maior que 20")]
    public decimal PrecoUnitario { get; set; }

    [Required(ErrorMessage = "Selecione a imagem principal do pacote")]
    public string UrlImagemPrincipal { get; set; }

    public string ImagemUpload { get; set; }

    public bool EmPromocao { get; set; }
    public decimal PrecoPromocional { get; set; }
    public bool Activo { get; set; }

    public List<Guid> ProdutosRelacionados { get; set; } = new List<Guid>();
    public List<string>? ImagensOpcionaisUrls { get; set; } = new List<string>();

}
