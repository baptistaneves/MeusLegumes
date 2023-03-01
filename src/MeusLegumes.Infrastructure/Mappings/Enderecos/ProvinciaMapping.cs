namespace MeusLegumes.Infrastructure.Mappings.Enderecos;

internal class ProvinciaMapping : IEntityTypeConfiguration<Provincia>
{
    public void Configure(EntityTypeBuilder<Provincia> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome)
            .HasColumnType("varchar(50)");

        builder.HasMany(p => p.Municipios)
          .WithOne(m => m.Provincia)
          .HasForeignKey(m => m.ProvinciaId);

        builder.ToTable("Provincias");
    }
}

