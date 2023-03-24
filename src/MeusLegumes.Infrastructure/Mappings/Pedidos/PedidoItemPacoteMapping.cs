using MeusLegumes.Domain.Contexts.Pedidos.Entities;

namespace MeusLegumes.Infrastructure.Mappings.Pedidos;

internal class PedidoItemPacoteMapping : IEntityTypeConfiguration<PedidoItemPacote>
{
    public void Configure(EntityTypeBuilder<PedidoItemPacote> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(c => c.PacoteNome)
            .IsRequired()
            .HasColumnType("varchar(250)");

        builder.Property(p => p.ValorUnitario)
           .HasColumnType("decimal(18,2)")
           .IsRequired();


        builder.Property(p => p.Quantidade)
           .HasColumnType("int")
           .IsRequired();

        builder.ToTable("PedidoItensPacote");
    }
}
