using System.ComponentModel.DataAnnotations;

namespace MeusLegumes.Application.Contexts.Enderecos.Contracts;

public class CriarProvincia
{
    [Required(ErrorMessage = "O Nome da província deve ser informado")]
    [MinLength(1, ErrorMessage = "O Nome da província deve ter no minimo 4 caracteres")]
    public string Nome { get; set; }
}

