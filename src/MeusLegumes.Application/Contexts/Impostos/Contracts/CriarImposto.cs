using System.ComponentModel.DataAnnotations;

namespace MeusLegumes.Application.Contexts.Impostos.Contracts;

public class CriarImposto
{
    [Required(ErrorMessage = "A descrição do imposto deve ser informado")]
    public string Descricao { get; private set; }

    [Required(ErrorMessage = "A taxa do imposto deve ser informada")]
    [Range(1, int.MaxValue, ErrorMessage = "A taxa do imposto deve ser maior que zero")]
    public decimal Taxa { get; private set; }

    [Required(ErrorMessage = "O tipo de taxa do imposto deve ser informado")]
    public string TipoDeTaxa { get; private set; }
}

