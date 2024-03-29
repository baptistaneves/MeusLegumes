﻿namespace MeusLegumes.Domain.Contexts.Produtos.Entities;

public class ProdutoRelacionado : Entity
{
    public Guid ProdutoId { get; private set; }
    public Guid ProdutoRelacionadoId { get; private set; }

    public ProdutoRelacionado(Guid produtoRelacionadoId)
    {
        ProdutoRelacionadoId = produtoRelacionadoId;
    }

    public void AssociarAoProduto(Guid produtoId)
    {
        ProdutoId = produtoId;
    }

    //EF Rel.
    public Produto Produto { get; private set; }
}
