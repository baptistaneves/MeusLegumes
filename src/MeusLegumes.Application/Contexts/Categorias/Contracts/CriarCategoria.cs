using System.ComponentModel.DataAnnotations;

namespace MeusLegumes.Application.Contexts.Categorias.Contracts;

public class CriarCategoria
{
    [Required(ErrorMessage = "O nome da categoria deve ser informado")]
    [MinLength(1, ErrorMessage = "O nome da categoria deve ter no minimo 4 caracteres")]
    public string Descricao { get; set; }
}

