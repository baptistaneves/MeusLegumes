using Bogus;
using MeusLegumes.API.Tests.Config;
using MeusLegumes.Application.Contexts.Categorias.Contracts;

namespace MeusLegumes.API.Tests.Contexts.Categorias;

[CollectionDefinition(nameof(CategoriaIntegrationTestsCollection))]
public class CategoriaIntegrationTestsCollection : ICollectionFixture<CategoriaIntegrationTestsFixture>
{

}
public class CategoriaIntegrationTestsFixture : IntegrationTestsFixtureBase<Program>
{
    private const string CATEGORIA_EXISTENTE = "Legumes";

    public CriarCategoria AdicionarNovaCategoriaValida()
    {
        return new Faker<CriarCategoria>("pt_PT")
            .CustomInstantiator(f => new CriarCategoria
            {
                Descricao = f.Commerce.ProductName()
            });
    }

    public CriarCategoria AdicionarNovaCategoriaInValida()
    {
        return new Faker<CriarCategoria>("pt_PT")
            .CustomInstantiator(f => new CriarCategoria
            {
                Descricao = f.Lorem.Letter(3)
            });
    }

    public CriarCategoria AdicionarCategoriaExistente()
    {
        return new CriarCategoria { Descricao = CATEGORIA_EXISTENTE };
    }

    public CriarCategoria AdicionarCategoriaSemDescricao()
    {
        return new CriarCategoria { };
    }
}

