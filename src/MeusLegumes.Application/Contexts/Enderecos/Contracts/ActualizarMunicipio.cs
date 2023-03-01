using System.ComponentModel.DataAnnotations;

namespace MeusLegumes.Application.Contexts.Enderecos.Contracts;

public class ActualizarMunicipio
{
    public Guid Id { get; set; }
    public Guid ProvinciaId { get; set; }

    [Required(ErrorMessage = "O Nome do munícipio deve ser informado")]
    [MinLength(1, ErrorMessage = "O Nome do munícipio deve ter no minimo 4 caracteres")]
    public string Nome { get; set; }
}

