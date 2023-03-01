namespace MeusLegumes.Domain.Contexts.Categorias.Entities;

public class Categoria : Entity
{
    public string Descricao { get; private set; }

    public Categoria(string descricao)
    {
        Descricao = descricao;
    }

    //For EF
    public IEnumerable<Produto> Produtos { get; private set; }
    public Categoria() { }
}