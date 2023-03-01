namespace MeusLegumes.Domain.Contexts.Enderecos.Entities;

public class Municipio : Entity
{
    public Guid ProvinciaId { get; private set; }
    public string Nome { get; private set; }

    //EF Rel.
    public Provincia Provincia { get; private set; }
    public IEnumerable<Cliente> Clientes { get; private set; }

    public Municipio(Guid provinciaId, string nome)
    {
        ProvinciaId = provinciaId;
        Nome = nome;
    }
}

