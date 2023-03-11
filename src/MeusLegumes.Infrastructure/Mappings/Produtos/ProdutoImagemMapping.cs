namespace MeusLegumes.Infrastructure.Mappings.Produtos;

internal partial class ProdutoMapping
{
    internal class ProdutoImagemMapping : IEntityTypeConfiguration<ProdutoImagem>
    {
        public void Configure(EntityTypeBuilder<ProdutoImagem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.UrlImagem)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.ToTable("ProdutoImagens");
        }
    }
}
