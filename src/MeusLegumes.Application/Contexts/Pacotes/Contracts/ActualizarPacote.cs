using System.ComponentModel.DataAnnotations;

namespace MeusLegumes.Application.Contexts.Pacotes.Contracts;

public class ActualizarPacote
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Informe o nome")]
    public string Nome { get; set; }

    public string Descricao { get; set; }

    [Required(ErrorMessage = "Informe o preço unitário")]
    [Range(20, int.MaxValue, ErrorMessage = "O valor do pacote deve ser maior que 20")]
    public decimal PrecoUnitario { get; set; }

    [Required(ErrorMessage = "Selecione uma imagem para o pacote")]
    public string ImagemUrl { get; set; }

    public bool EmPromocao { get; set; }
    public decimal PrecoPromocional { get; set; }
    public bool Activo { get; set; }

    public IEnumerable<ActualizarPacoteProduto> PacoteProdutos { get; set; }

}

