﻿namespace MeusLegumes.Infrastructure.Mappings.Unidades;

internal class UnidadeMapping : IEntityTypeConfiguration<Unidade>
{
    public void Configure(EntityTypeBuilder<Unidade> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Descricao)
           .HasColumnType("varchar(150)");

        builder.HasMany(u => u.Produtos)
           .WithOne(p => p.Unidade)
           .HasForeignKey(p => p.UnidadeId)
           .OnDelete(DeleteBehavior.ClientSetNull);

        builder.ToTable("Unidades");
    }
}
