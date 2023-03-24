using MeusLegumes.Domain.Contexts.Pedidos.Entities;

namespace MeusLegumes.Infrastructure.Mappings.Pedidos;

internal class PedidoMapping : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.ValorTotal)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.PedidoStatus)
           .HasColumnType("int")
           .IsRequired();

        builder.Property(p => p.Codigo)
           .HasDefaultValue("NEXT VALUE FOR CODIGO_PEDIDO");

        builder.HasMany(p => p.PedidoItensProduto)
            .WithOne(pi => pi.Pedido)
            .HasForeignKey(pi => pi.PedidoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.PedidoItensPacote)
            .WithOne(pi => pi.Pedido)
            .HasForeignKey(pi => pi.PedidoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("Pedidos");
    }
}
