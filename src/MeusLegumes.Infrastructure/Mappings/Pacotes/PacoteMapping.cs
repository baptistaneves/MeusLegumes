namespace MeusLegumes.Infrastructure.Mappings.Pacotes;

internal partial class PacoteMapping : IEntityTypeConfiguration<Pacote>
{
    public void Configure(EntityTypeBuilder<Pacote> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(p => p.Nome)
           .IsRequired()
           .HasColumnType("varchar(255)");

        builder.Property(p => p.ImagemUrl)
          .IsRequired()
          .HasColumnType("varchar(255)");

        builder.Property(p => p.Descricao)
          .HasColumnType("text");


        builder.Property(p => p.PrecoUnitario)
          .HasColumnType("decimal(18,2)")
          .IsRequired();

        builder.Property(p => p.PrecoPromocional)
         .HasColumnType("decimal(18,2)");

        builder.Property(p => p.EmPromocao)
         .HasColumnType("bit");

        builder.Property(p => p.Activo)
         .HasColumnType("bit");

        builder.HasMany(p => p.PacotesProduto)
          .WithOne(pr => pr.Pacote)
          .HasForeignKey(pr => pr.PacoteId);

        builder.ToTable("Pacotes");
    }
}
