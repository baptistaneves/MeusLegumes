namespace MeusLegumes.Infrastructure.Mappings.Impostos;

internal class ImpostoMapping : IEntityTypeConfiguration<Imposto>
{
    public void Configure(EntityTypeBuilder<Imposto> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Descricao)
            .HasColumnType("varchar(200)");

        builder.Property(i => i.TipoDeTaxa)
          .HasColumnType("varchar(100)");

        builder.Property(i => i.Taxa)
          .HasColumnType("decimal(18,2)");

        builder.HasMany(i => i.Produtos)
            .WithOne(p => p.Imposto)
            .HasForeignKey(p => p.ImpostoId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.ToTable("Impostos");
    }
}