namespace MeusLegumes.Infrastructure.Mappings.Produtos;

internal partial class ProdutoMapping : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(p => p.Nome)
            .IsRequired()
            .HasColumnType("varchar(255)");

        builder.Property(p => p.Descricao)
           .IsRequired(false)
           .HasColumnType("text");

        builder.Property(p => p.Observacao)
           .IsRequired(false)
           .HasColumnType("text");

        builder.Property(p => p.UrlImagemPrincipal)
           .IsRequired()
           .HasColumnType("varchar(255)");

        builder.Property(p => p.PrecoUnitario)
           .IsRequired()
           .HasColumnType("decimal(18,2)");

        builder.Property(p => p.PrecoPromocional)
            .IsRequired(false)
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.Tipo)
           .IsRequired()
           .HasColumnType("int");

        builder.Property(p => p.EmPromocao)
           .HasColumnType("bit");

        builder.Property(p => p.Destaque)
           .HasColumnType("bit");

        builder.Property(p => p.EmEstoque)
           .HasColumnType("bit");

        builder.Property(p => p.Activo)
           .HasColumnType("bit");

        builder.Property(p => p.NovoLancamento)
          .HasColumnType("bit");

        builder.Property(p => p.MaisVendido)
         .HasColumnType("bit");

        builder.Property(p => p.MaisProcurado)
        .HasColumnType("bit");


        builder.ToTable("Produtos");
    }
}
