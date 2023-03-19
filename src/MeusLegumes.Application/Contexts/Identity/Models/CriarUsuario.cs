﻿using System.ComponentModel.DataAnnotations;

namespace MeusLegumes.Application.Contexts.Identity.Models;

public class CriarUsuario
{
    [Required(ErrorMessage = "O nome deve ser informado")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O email deve ser informado")]
    [EmailAddress(ErrorMessage = "O email informado é inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "A senha deve ser informada")]
    public string Senha { get; set; }

    [Required(ErrorMessage = "O perfil deve ser informado")]
    public string Perfil { get; set; }
}
