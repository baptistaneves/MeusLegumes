using System.ComponentModel.DataAnnotations;

namespace MeusLegumes.Application.Contexts.Identity.Models;

public class AlterarSenha
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Informe a Senha Actual")]
    public string SenhaActual { get; set; }

    [Required(ErrorMessage = "Informe a Nova Senha")]
    public string NovaSenha { get; set; }
}
