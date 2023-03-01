namespace MeusLegumes.Domain.Contexts.Impostos.Entities;

public class MotivoIsencaoIva : Entity
{

    public string CodigoInterno { get; private set; }
    public string MencaoFactura { get; private set; }
    public string NormaLegalAplicavel { get; private set; }
    public string Motivo { get; private set; }

    public MotivoIsencaoIva(string codigoInterno, string mencaoFactura, string normaLegalAplicavel, string motivo)
    {
        CodigoInterno = codigoInterno;
        MencaoFactura = mencaoFactura;
        NormaLegalAplicavel = normaLegalAplicavel;
        Motivo = motivo;
    }

    //For EF
    public IEnumerable<Produto> Produtos { get; private set; }
    public MotivoIsencaoIva() { }
}
