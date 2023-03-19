namespace MeusLegumes.Infrastructure.Mappings.Produtos;

internal partial class ProdutoMapping
{
    internal class ProdutoRelacionadoMapping : IEntityTypeConfiguration<ProdutoRelacionado>
    {
        public void Configure(EntityTypeBuilder<ProdutoRelacionado> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(pr => pr.Produto)
              .WithMany(p => p.ProdutosRelacionado)
              .HasForeignKey(pr => pr.ProdutoId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("ProdutoRelacionados");
        }
    }
}
