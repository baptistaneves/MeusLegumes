using MeusLegumes.Application.Contexts.Categorias.Services;
using MeusLegumes.Domain.Contexts.Categorias.Repositories;
using Moq;

namespace MeusLegumes.Application.Tests.Contexts.Categorias;

[Collection(nameof(CategoriaAppServiceTestsCollection))]
public class CategoriaAppServiceTests
{
    private readonly CategoriaAppServiceTestsFixture _categoriaFixture;
    private readonly CategoriaAppService _categoriaAppService;

    public CategoriaAppServiceTests(CategoriaAppServiceTestsFixture categoriaFixture)
    {
        _categoriaFixture = categoriaFixture;
        _categoriaAppService = _categoriaFixture.ObterCategoriaAppService();

        _categoriaFixture.Mocker.GetMock<ICategoriaRepository>().Setup(r => r.UnitOfWork.SaveChangesAsync(CancellationToken.None)).Returns(Task.FromResult(true));
    }

    [Fact(DisplayName = "Adicionar Categoria com Sucesso")]
    [Trait("Categoria", "Categoria Service Tests")]
    public async Task CategoriaService_Adicionar_DeveExecutarComSucesso()
    {
        //Arrange
        var categoria = _categoriaFixture.AdicionarCategoriaValida();

        //Act
        await _categoriaAppService.Adicionar(categoria, CancellationToken.None);

        //Assert
        _categoriaFixture.Mocker.GetMock<ICategoriaRepository>().Verify(r => r.UnitOfWork.SaveChangesAsync(CancellationToken.None), Times.Once);
    }

    [Fact(DisplayName = "Adicionar categoria com  Descrição sem o minimo de caracteres exigido")]
    [Trait("Categoria", "Categoria Service Tests")]
    public async Task CategoriaService_Adicionar_DescricaoDaCategoriaComMinimoDeCaracteresExigido()
    {
        //Arrange
        var categoria = _categoriaFixture.AdicionarCategoriaInValida();

        //Act
        await _categoriaAppService.Adicionar(categoria, CancellationToken.None);

        //Assert
        _categoriaFixture.Mocker.GetMock<ICategoriaRepository>().Verify(r => r.UnitOfWork.SaveChangesAsync(CancellationToken.None), Times.Never);
    }

    [Fact(DisplayName = "Adicionar categoria com Descrição vazia")]
    [Trait("Categoria", "Categoria Service Tests")]
    public async Task CategoriaService_Adicionar_DescricaoDaCategoriaVazia()
    {
        //arrange
        var categoria = _categoriaFixture.AdicionarCategoriaComDescricaoVazia();

        //Act
        await _categoriaAppService.Adicionar(categoria, CancellationToken.None);

        //Assert
        _categoriaFixture.Mocker.GetMock<ICategoriaRepository>().Verify(r => r.UnitOfWork.SaveChangesAsync(CancellationToken.None), Times.Never);
    }

    [Fact(DisplayName = "Atualizar categoria com  Descrição sem o minimo de caracteres exigido")]
    [Trait("Categoria", "Categoria Service Tests")]
    public async Task CategoriaService_Atualizar_DescricaoDaCategoriaComMinimoDeCaracteresExigido()
    {
        //Arrange
        var categoria = _categoriaFixture.AtualizarCategoriaInValida();

        //Act
        await _categoriaAppService.Actualizar(categoria, CancellationToken.None);

        //Assert
        _categoriaFixture.Mocker.GetMock<ICategoriaRepository>().Verify(r => r.UnitOfWork.SaveChangesAsync(CancellationToken.None), Times.Never);
    }

    [Fact(DisplayName = "Atualizar categoria com Descrição vazia")]
    [Trait("Categoria", "Categoria Service Tests")]
    public async Task CategoriaService_Atualizar_DescricaoDaCategoriaVazia()
    {
        //arrange
        var categoria = _categoriaFixture.AtualizarCategoriaComDescricaoVazia();

        //Act
        await _categoriaAppService.Actualizar(categoria, CancellationToken.None);

        //Assert
        _categoriaFixture.Mocker.GetMock<ICategoriaRepository>().Verify(r => r.UnitOfWork.SaveChangesAsync(CancellationToken.None), Times.Never);
    }

    [Fact(DisplayName = "Atualizar categoria com o ID invalido")]
    [Trait("Categoria", "Categoria Service Tests")]
    public async Task CategoriaService_Atualizar_IdDaCategoriaInvalido()
    {
        //arrange
        var categoria = _categoriaFixture.AtualizarCategoriaComIdInValido();

        //Act
        await _categoriaAppService.Actualizar(categoria, CancellationToken.None);

        //Assert
        _categoriaFixture.Mocker.GetMock<ICategoriaRepository>().Verify(r => r.UnitOfWork.SaveChangesAsync(CancellationToken.None), Times.Never);
    }
}
