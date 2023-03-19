using System.ComponentModel.DataAnnotations;

namespace MeusLegumes.Application.Contexts.Identity.Models;

public class Login
{
    [Required(ErrorMessage = "O email deve ser informado")]
    [EmailAddress(ErrorMessage = "O email informado é inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "A senha deve ser informada")]
    public string Senha { get; set; }
}
