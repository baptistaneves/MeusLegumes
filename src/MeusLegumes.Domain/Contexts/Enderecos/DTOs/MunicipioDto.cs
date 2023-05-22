namespace MeusLegumes.Domain.Contexts.Enderecos.DTOs;

public class MunicipioDto
{
    public Guid Id { get; set; }
    public Guid ProvinciaId { get; set; }
    public string Provincia { get; set; }
    public string Nome { get; set; }
}
