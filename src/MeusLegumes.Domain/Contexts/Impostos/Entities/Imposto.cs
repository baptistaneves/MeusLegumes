namespace MeusLegumes.Domain.Contexts.Impostos.Entities;

public class Imposto : Entity
{
    public string Descricao { get; private set; }
    public decimal Taxa { get; private set; }
    public string TipoDeTaxa { get; private set; }

    public Imposto(string descricao, decimal taxa, string tipoDeTaxa)
    {
        Descricao = descricao;
        Taxa = taxa;
        TipoDeTaxa = tipoDeTaxa;
    }

    //For EF
    public IEnumerable<Produto> Produtos { get; private set; }
    public Imposto() { }    
}
