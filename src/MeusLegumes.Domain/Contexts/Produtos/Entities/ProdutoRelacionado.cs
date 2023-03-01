namespace MeusLegumes.Domain.Contexts.Produtos.Entities
{
    public class ProdutoRelacionado
    {
        public Guid ProdutoPrincipalId { get; private set; }
        public Guid ProdutoRelacionadoId { get; private set; }

        public ProdutoRelacionado(Guid produtoPrincipalId, Guid produtoRelacionadoId)
        {
            ProdutoPrincipalId = produtoPrincipalId;
            ProdutoRelacionadoId = produtoRelacionadoId;
        }
    }
}
