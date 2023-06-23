using Bogus;
using MeusLegumes.Application.Contexts.Categorias.Contracts;
using MeusLegumes.Application.Contexts.Categorias.Services;
using MeusLegumes.Infrastructure.Repositories.Categorias;
using Moq.AutoMock;

namespace MeusLegumes.Application.Tests.Contexts.Categorias;

[CollectionDefinition(nameof(CategoriaAppServiceTestsCollection))]
public class CategoriaAppServiceTestsCollection : ICollectionFixture<CategoriaAppServiceTestsFixture>
{
}

public class CategoriaAppServiceTestsFixture
{
    public CategoriaAppService CategoriaAppService;
    public AutoMocker Mocker;

    public CategoriaAppService ObterCategoriaAppService()
    {
        Mocker = new AutoMocker();
        CategoriaAppService = Mocker.CreateInstance<CategoriaAppService>();

        return CategoriaAppService;
    }

    public IEnumerable<Categoria> ObterCategorias()
    {
        return GerarCategorias(10);
    }


    public IEnumerable<Categoria> GerarCategorias(int quantidade)
    {
        var categorias = new Faker<Categoria>("pt_PT")
            .CustomInstantiator(f => new Categoria(f.Lorem.Word()));

        return categorias.Generate(quantidade);
    }

    public CriarCategoria AdicionarCategoriaValida()
    {
        var categoria = new Faker<CriarCategoria>("pt_PT")
            .CustomInstantiator(f => new CriarCategoria { Descricao = f.Lorem.Letter(4) });

        return categoria;
    }

    public CriarCategoria AdicionarCategoriaInValida()
    {
        var categoria = new Faker<CriarCategoria>("pt_PT")
            .CustomInstantiator(f => new CriarCategoria { Descricao = f.Lorem.Letter(2) });

        return categoria;
    }

    public CriarCategoria AdicionarCategoriaComDescricaoVazia()
    {
        var categoria =  new CriarCategoria { Descricao = "" };

        return categoria;
    }

    public ActualizarCategoria AtualizarCategoriaValida()
    {
        var categoria = new Faker<ActualizarCategoria>("pt_PT")
            .CustomInstantiator(f => new ActualizarCategoria { Id = Guid.NewGuid(), Descricao = f.Lorem.Letter(4) });

        return categoria;
    }

    public ActualizarCategoria AtualizarCategoriaInValida()
    {
        var categoria = new Faker<ActualizarCategoria>("pt_PT")
            .CustomInstantiator(f => new ActualizarCategoria { Id = Guid.NewGuid(), Descricao = f.Lorem.Letter(2) });

        return categoria;
    }

    public ActualizarCategoria AtualizarCategoriaComDescricaoVazia()
    {
        var categoria = new ActualizarCategoria { Id = Guid.NewGuid(), Descricao = "" };

        return categoria;
    }

    public ActualizarCategoria AtualizarCategoriaComIdInValido()
    {
        var categoria = new Faker<ActualizarCategoria>("pt_PT")
            .CustomInstantiator(f => new ActualizarCategoria { Id = Guid.Empty, Descricao = f.Lorem.Letter(5) });

        return categoria;
    }
}
