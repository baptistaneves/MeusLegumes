namespace MeusLegumes.Infrastructure.Mappings.Enderecos;

internal class MunicipioMapping : IEntityTypeConfiguration<Municipio>
{
    public void Configure(EntityTypeBuilder<Municipio> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Nome)
            .HasColumnType("varchar(50)");

        builder.HasMany(m => m.Clientes)
         .WithOne(c => c.Municipio)
         .HasForeignKey(c => c.MunicipioId)
         .OnDelete(DeleteBehavior.ClientSetNull);

        builder.ToTable("Municipios");
    }
}

