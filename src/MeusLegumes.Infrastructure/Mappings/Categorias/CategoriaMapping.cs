namespace MeusLegumes.Infrastructure.Mappings.Categorias;

public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Descricao)
            .HasColumnType("varchar(150)");

        builder.HasMany(c => c.Produtos)
            .WithOne(p => p.Categoria)
            .HasForeignKey(p => p.CategoriaId);
            
        builder.ToTable("Categorias");
    }
}
