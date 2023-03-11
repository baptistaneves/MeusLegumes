using System.ComponentModel.DataAnnotations;

namespace MeusLegumes.Application.Contexts.Clientes.Contracts;

public class ActualizarCliente
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O munícipio deve ser informado")]
    public Guid MunicipioId { get; set; }

    [Required(ErrorMessage = "O nome do deve ser informado")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O tipo de deve ser informado")]
    public string Tipo { get; set; }

    [Required(ErrorMessage = "O Nº de Contribuinte deve ser informado")]
    public string NumeroContribuinte { get; set; }

    [Required(ErrorMessage = "O telefone principal deve ser informado")]
    public string TelefonePrincipal { get; set; }

    public string TelefoneAlternativo { get; set; }

    [Required(ErrorMessage = "O email deve ser informado")]
    [EmailAddress(ErrorMessage = "O email informado é inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O nome da rua deve ser informado")]
    public string Rua { get; set; }

    public string Casa { get; set; }

    public string CodigoPostal { get; set; }

    [Required(ErrorMessage = "O ponto de referência deve ser informado")]
    public string PontoDeReferencia { get; set; }
}

