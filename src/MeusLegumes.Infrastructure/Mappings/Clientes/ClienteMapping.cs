namespace MeusLegumes.Infrastructure.Mappings.Clientes
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Tipo)
                .HasColumnType("varchar(50)");

            builder.Property(c => c.NumeroContribuinte)
                .HasColumnType("varchar(20)");

            builder.Property(c => c.TelefonePrincipal)
                .HasColumnType("varchar(20)");

            builder.Property(c => c.TelefoneAlternativo)
                .IsRequired(false)
                .HasColumnType("varchar(20)");

            builder.Property(c => c.Email)
                .HasColumnType("varchar(255)");

            builder.Property(c => c.Rua)
                .IsRequired(false)
                .HasColumnType("varchar(255)");

            builder.Property(c => c.Casa)
                .IsRequired(false)
                .HasColumnType("varchar(20)");

            builder.Property(c => c.CodigoPostal)
                .IsRequired(false)
                .HasColumnType("varchar(200)");

            builder.Property(c => c.PontoDeReferencia)
                .HasColumnType("varchar(1000)");

            builder.ToTable("Clientes");
        }
    }
}
