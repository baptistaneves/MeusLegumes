using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MeusLegumes.Application.Contexts.Pacotes.Contracts;

public class CriarPacote
{
    [Required(ErrorMessage = "Informe o nome")]
    public string Nome { get; set; }

    public string Descricao { get; set; }

    [Required(ErrorMessage = "Informe o preço unitário")]
    [Range(20, int.MaxValue, ErrorMessage = "O valor do pacote deve ser maior que 20")]
    public decimal PrecoUnitario { get; set; }

    public IFormFile Imagem { get; set; }

    public string ImagemUrl { get; private set; }

    public bool EmPromocao { get; set; }
    public decimal PrecoPromocional { get; set; }
    public bool Activo { get; set; }

    public IEnumerable<CriarPacoteProduto> PacoteProdutos { get; set; }

    public void AdicionarImagemUrl(string imagemUrl) => ImagemUrl = imagemUrl;
}
