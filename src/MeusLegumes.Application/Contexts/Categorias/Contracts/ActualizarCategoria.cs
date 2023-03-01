using System.ComponentModel.DataAnnotations;

namespace MeusLegumes.Application.Contexts.Categorias.Contracts;

public class ActualizarCategoria
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O nome da categoria deve ser informado")]
    [MinLength(1, ErrorMessage = "A Descrição/Nome da categoria deve ter no minimo 4 caracteres")]
    public string Descricao { get; set; }
}

