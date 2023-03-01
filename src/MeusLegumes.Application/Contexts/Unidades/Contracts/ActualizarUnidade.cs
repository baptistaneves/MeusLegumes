using System.ComponentModel.DataAnnotations;

namespace MeusLegumes.Application.Contexts.Unidades.Contracts;

public class ActualizarUnidade
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "A Descrição/Nome da unidade deve ser informado")]
    [MinLength(1, ErrorMessage = "A Descrição/Nome da unidade deve ter no minimo 4 caracteres")]
    public string Descricao { get; set; }
}

