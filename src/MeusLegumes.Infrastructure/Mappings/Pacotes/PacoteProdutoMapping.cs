namespace MeusLegumes.Infrastructure.Mappings.Pacotes;

internal partial class PacoteMapping
{
    internal class PacoteProdutoMapping : IEntityTypeConfiguration<PacoteProduto>
    {
        public void Configure(EntityTypeBuilder<PacoteProduto> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Quantidade)
                .IsRequired()
                .HasColumnType("int");

            builder.ToTable("PacotesProduto");
        }
    }
}
