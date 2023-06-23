using MeusLegumes.Application.Contexts.Produtos.Services;
using MeusLegumes.Domain.Contexts.Produtos.Repositories;
using Moq;

namespace MeusLegumes.Application.Tests.Contexts.Produtos;

[Collection(nameof(ProdutoAppServiceColletion))]
public class ProdutoAppServiceTests
{
    private ProdutoAppServiceTestsFixture _fixture;
    private ProdutoAppService _produtoAppService;

    public ProdutoAppServiceTests(ProdutoAppServiceTestsFixture fixture)
    {
        _fixture = fixture;
        _produtoAppService= _fixture.ObterProdutoAppService();

        _fixture.Mocker.GetMock<IProdutoRepository>().Setup(r => r.UnitOfWork.SaveChangesAsync(CancellationToken.None)).Returns(Task.FromResult(true));
    }

    [Fact(DisplayName = "Adicionar produto com sucesso")]
    [Trait("Produto", "Produto Service Tests")]
    public async Task ProdutoService_Adicionar_DeveExecutarComSucesso()
    {
        //Arrange
        var produto = _fixture.ObterProdutoValido();

        //Act
        await _produtoAppService.Adicionar(produto, CancellationToken.None);

        //Assert
        _fixture.Mocker.GetMock<IProdutoRepository>().Verify(v => v.UnitOfWork.SaveChangesAsync(CancellationToken.None), Times.Once);
    }

    [Fact(DisplayName = "Adicionar produto com falha")]
    [Trait("Produto", "Produto Service Tests")]
    public async Task ProdutoService_Adicionar_DeveExecutarComFalha()
    {
        //Arrange
        var produto = _fixture.ObterProdutoInValido();

        //Act
        await _produtoAppService.Adicionar(produto, CancellationToken.None);

        //Assert
        _fixture.Mocker.GetMock<IProdutoRepository>().Verify(v => v.UnitOfWork.SaveChangesAsync(CancellationToken.None), Times.Never);
    }

    [Fact(DisplayName = "Adicionar produto em promoção sem preço promocional")]
    [Trait("Produto", "Produto Service Tests")]
    public async Task ProdutoService_Adicionar_ProdutoEmPromocaoSemPrecoPromocionalDeveFalhar()
    {
        //Arrange
        var produto = _fixture.ObterProdutoEmPromocaoSemPrecoPromocional();

        //Act
        await _produtoAppService.Adicionar(produto, CancellationToken.None);

        //Assert
        _fixture.Mocker.GetMock<IProdutoRepository>().Verify(v => v.UnitOfWork.SaveChangesAsync(CancellationToken.None), Times.Never);
    }

    [Fact(DisplayName = "Adicionar produto com preço unitário abaixo de 20")]
    [Trait("Produto", "Produto Service Tests")]
    public async Task ProdutoService_Adicionar_ProdutoComPrecoUnitarioAbaixode20DeveFalhar()
    {
        //Arrange
        var produto = _fixture.ObterProdutoComPrecoUnitarioAbaixoDe20();

        //Act
        await _produtoAppService.Adicionar(produto, CancellationToken.None);

        //Assert
        _fixture.Mocker.GetMock<IProdutoRepository>().Verify(v => v.UnitOfWork.SaveChangesAsync(CancellationToken.None), Times.Never);
    }
}
