namespace MeusLegumes.Infrastructure.Mappings.Pacotes;

internal partial class PacoteMapping
{
    internal class PacoteItemMapping : IEntityTypeConfiguration<PacoteItem>
    {
        public void Configure(EntityTypeBuilder<PacoteItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Quantidade)
                .IsRequired()
                .HasColumnType("int");

            builder.ToTable("PacoteItems");
        }
    }
}
