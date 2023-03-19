using System.ComponentModel.DataAnnotations;

namespace MeusLegumes.Application.Contexts.Pacotes.Contracts;

public class CriarPacoteItem
{
    [Required(ErrorMessage = "Informe o pruduto para o pacote")]
    public Guid ProdutoId { get; set; }

    [Required(ErrorMessage = "Infome a unidade do produto")]
    public Guid UnidadeId { get; set; }

    [Required(ErrorMessage = "Infome a quantidade de produto a ser adicionado no pacote")]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero (0)")]
    public int Quantidade { get; set; }
}
