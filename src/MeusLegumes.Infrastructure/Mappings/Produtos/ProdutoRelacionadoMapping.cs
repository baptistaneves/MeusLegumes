namespace MeusLegumes.Infrastructure.Mappings.Produtos;

internal partial class ProdutoMapping
{
    internal class ProdutoRelacionadoMapping : IEntityTypeConfiguration<ProdutoRelacionado>
    {
        public void Configure(EntityTypeBuilder<ProdutoRelacionado> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable("ProdutosRelacionado");
        }
    }
}
