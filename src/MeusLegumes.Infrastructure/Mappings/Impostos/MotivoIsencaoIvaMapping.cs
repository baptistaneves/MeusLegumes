namespace MeusLegumes.Infrastructure.Mappings.Impostos;

internal class MotivoIsencaoIvaMapping : IEntityTypeConfiguration<MotivoIsencaoIva>
{
    public void Configure(EntityTypeBuilder<MotivoIsencaoIva> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.CodigoInterno)
            .HasColumnType("varchar(100)");

        builder.Property(i => i.MencaoFactura)
          .HasColumnType("varchar(100)");

        builder.Property(i => i.NormaLegalAplicavel)
          .HasColumnType("varchar(100)");

        builder.Property(i => i.Motivo)
         .HasColumnType("varchar(500)");

        builder.HasMany(m => m.Produtos)
           .WithOne(p => p.MotivoIsencaoIva)
           .HasForeignKey(p => p.MotivoId)
           .OnDelete(DeleteBehavior.ClientSetNull);

        builder.ToTable("MotivosIsencaoIva");
    }
}
