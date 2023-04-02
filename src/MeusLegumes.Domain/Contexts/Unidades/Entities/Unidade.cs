namespace MeusLegumes.Domain.Contexts.Unidades.Entities;

public class Unidade : Entity
{
    public string Descricao { get; private set; }

    public Unidade(string descricao)
    {
        Descricao = descricao;
    }

    //For EF
    public IEnumerable<Produto> Produtos { get; private set; }

    public Unidade() { }
}
