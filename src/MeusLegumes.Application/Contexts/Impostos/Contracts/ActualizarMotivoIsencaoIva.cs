using System.ComponentModel.DataAnnotations;

namespace MeusLegumes.Application.Contexts.Impostos.Contracts;

public class ActualizarMotivoIsencaoIva
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O código intero do motivo deve ser informado")]
    public string CodigoInterno { get; set; }

    [Required(ErrorMessage = "A menção pela factura deve ser informada")]
    public string MencaoFactura { get; set; }

    [Required(ErrorMessage = "A norma legal aplicavel deve ser informada")]
    public string NormaLegalAplicavel { get; set; }

    [Required(ErrorMessage = "Informe o motivo")]
    public string Motivo { get; set; }
}

