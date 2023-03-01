namespace MeusLegumes.Domain.Contexts.Produtos.Entities
{
    public class ProdutoImagem : Entity
    {
        public Guid ProdutoId { get; private set; }
        public Guid UrlImagem { get; private set; }

        public ProdutoImagem(Guid produtoId, Guid urlImagem)
        {
            ProdutoId = produtoId;
            UrlImagem = urlImagem;
        }
    }
}
