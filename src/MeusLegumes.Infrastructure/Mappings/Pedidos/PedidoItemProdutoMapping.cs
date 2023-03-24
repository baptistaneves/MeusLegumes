using MeusLegumes.Domain.Contexts.Pedidos.Entities;

namespace MeusLegumes.Infrastructure.Mappings.Pedidos;

internal class PedidoItemProdutoMapping : IEntityTypeConfiguration<PedidoItem>
{
    public void Configure(EntityTypeBuilder<PedidoItem> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(c => c.ProdutoNome)
            .IsRequired()
            .HasColumnType("varchar(250)");

        builder.Property(p => p.ValorUnitario)
           .HasColumnType("decimal(18,2)")
           .IsRequired();


        builder.Property(p => p.Quantidade)
           .HasColumnType("int")
           .IsRequired();

        builder.ToTable("PedidoItensProduto");
    }
}
