namespace MeusLegumes.Domain.Contexts.Enderecos.Entities;

public class Provincia : Entity
{
    public string Nome { get; private set; }

    public IEnumerable<Municipio> Municipios { get; private set; }

    public Provincia(string nome)
    {
        Nome = nome;
    }

    public Provincia() { }  
}

