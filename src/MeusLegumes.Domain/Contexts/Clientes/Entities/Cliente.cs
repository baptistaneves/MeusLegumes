namespace MeusLegumes.Domain.Contexts.Clientes.Entities;

public class Cliente : Entity
{
    public Guid MunicipioId { get; private set; }
    public string UserIdentityId { get; private set; }
    public string Nome { get; private set; }
    public string Tipo { get; private set; }
    public string NumeroContribuinte { get; private set; }
    public string TelefonePrincipal { get; private set; }
    public string TelefoneAlternativo { get; private set; }
    public string Email { get; private set; }
    public string Rua { get; private set; }
    public string Casa { get; private set; }
    public string CodigoPostal { get; private set; }
    public string PontoDeReferencia { get; private set; }

    public Cliente(string nome, string userIdentityId, string tipo, string numeroContribuinte, string telefonePrincipal, string telefoneAlternativo, string email, Guid municipioId, string rua, string casa, string codigoPostal, string pontoDeReferencia)
    {
        Nome = nome;
        UserIdentityId = userIdentityId;
        Tipo = tipo;
        NumeroContribuinte = numeroContribuinte;
        TelefonePrincipal = telefonePrincipal;
        TelefoneAlternativo = telefoneAlternativo;
        Email = email;
        MunicipioId = municipioId;
        Rua = rua;
        Casa = casa;
        CodigoPostal = codigoPostal;
        PontoDeReferencia = pontoDeReferencia;
    }

    public Cliente() { }

    //EF Rel.
    public Municipio Municipio { get; private set; }
}
