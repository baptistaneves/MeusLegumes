namespace MeusLegumes.Domain.Contexts.Pacotes.Entities;

public class PacoteProduto : Entity
{
    public Guid PacoteId { get; private set; }
    public Guid ProdutoId { get; private set; }
    public Guid UnidadeId { get; private set; }
    public int Quantidade { get; private set; }

    //EF Rel.
    public Pacote Pacote { get; private set; }
    public Produto Produto { get; private set; }
    public Unidade Unidade { get; private set; }

    //For EF
    public PacoteProduto() { }

    public PacoteProduto(Guid produtoId, Guid unidadeId, int quantidade)
    {
        ProdutoId = produtoId;
        UnidadeId = unidadeId;
        Quantidade = quantidade;
    }

    public void AssociarAoPacote(Guid pacoteId)
    {
        PacoteId = pacoteId;
    }

    public void AdicionarUnidades(int quantidade)
    {
        Quantidade += quantidade;
    }
}
