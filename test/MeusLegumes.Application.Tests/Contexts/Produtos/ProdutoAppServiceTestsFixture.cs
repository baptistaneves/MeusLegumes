using Bogus;
using MeusLegumes.Application.Contexts.Produtos.Contracts;
using MeusLegumes.Application.Contexts.Produtos.Services;
using Moq.AutoMock;

namespace MeusLegumes.Application.Tests.Contexts.Produtos
{
    [CollectionDefinition(nameof(ProdutoAppServiceColletion))]
    public class ProdutoAppServiceColletion : ICollectionFixture<ProdutoAppServiceTestsFixture>
    {
    }
    public class ProdutoAppServiceTestsFixture
    {
        public AutoMocker Mocker;
        public ProdutoAppService ProdutoAppService;

        public ProdutoAppService ObterProdutoAppService()
        {
            Mocker = new AutoMocker();
            ProdutoAppService = Mocker.CreateInstance<ProdutoAppService>();

            return ProdutoAppService;
        }

        public CriarProduto ObterProdutoValido()
        {
            var produto = new Faker<CriarProduto>("pt_PT")
                .CustomInstantiator(f => new CriarProduto
                {
                    CategoriaId = Guid.NewGuid(),
                    UnidadeId = Guid.NewGuid(),
                    ImpostoId = Guid.NewGuid(),
                    MotivoId = Guid.NewGuid(),
                    Nome = f.Commerce.ProductName(),
                    Descricao = f.Commerce.ProductDescription(),
                    PrecoUnitario = f.Commerce.Price(20, 2000, 2, "KZ").First(),
                    UrlImagemPrincipal = f.Image.LoremPixelUrl(),
                    ImagemUpload = f.Image.LoremPixelUrl(),
                    EmPromocao = true,
                    PrecoPromocional = f.Commerce.Price(20, 1000, 2, "KZ").First(),
                    Destaque = false,
                    NovoLancamento = false,
                    MaisVendido = false,
                    MaisProcurado = false,
                    EmEstoque = false,
                    Activo = true,
                    Observacao = f.Commerce.ProductDescription(),
                    ProdutosRelacionados = ObterProdutosRelacionadas(),
                    ImagensOpcionaisUrls = ObterImagensRelacionadas(),
                });

            return produto;
        }

        public CriarProduto ObterProdutoInValido()
        {
            var produto = new Faker<CriarProduto>("pt_PT")
                .CustomInstantiator(f => new CriarProduto
                {
                    CategoriaId = Guid.Empty,
                    UnidadeId = Guid.NewGuid(),
                    ImpostoId = Guid.NewGuid(),
                    MotivoId = Guid.NewGuid(),
                    Nome = "",
                    Descricao = f.Commerce.ProductDescription(),
                    PrecoUnitario = 0,
                    UrlImagemPrincipal = f.Image.LoremPixelUrl(),
                    ImagemUpload = f.Image.LoremPixelUrl(),
                    EmPromocao = true,
                    PrecoPromocional = f.Commerce.Price(20, 1000, 2, "KZ").First(),
                    Destaque = false,
                    NovoLancamento = false,
                    MaisVendido = false,
                    MaisProcurado = false,
                    EmEstoque = false,
                    Activo = true,
                    Observacao = f.Commerce.ProductDescription(),
                    ProdutosRelacionados = ObterProdutosRelacionadas(),
                    ImagensOpcionaisUrls = ObterImagensRelacionadas(),
                });

            return produto;
        }

        public CriarProduto ObterProdutoEmPromocaoSemPrecoPromocional()
        {
            var produto = new Faker<CriarProduto>("pt_PT")
                .CustomInstantiator(f => new CriarProduto
                {
                    CategoriaId = Guid.Empty,
                    UnidadeId = Guid.NewGuid(),
                    ImpostoId = Guid.NewGuid(),
                    MotivoId = Guid.NewGuid(),
                    Nome = f.Commerce.ProductName(),
                    Descricao = f.Commerce.ProductDescription(),
                    PrecoUnitario = f.Commerce.Price(20, 2000, 2, "KZ").First(),
                    UrlImagemPrincipal = f.Image.LoremPixelUrl(),
                    ImagemUpload = f.Image.LoremPixelUrl(),
                    EmPromocao = true,
                    PrecoPromocional = 0,
                    Destaque = false,
                    NovoLancamento = false,
                    MaisVendido = false,
                    MaisProcurado = false,
                    EmEstoque = false,
                    Activo = true,
                    Observacao = f.Commerce.ProductDescription(),
                    ProdutosRelacionados = ObterProdutosRelacionadas(),
                    ImagensOpcionaisUrls = ObterImagensRelacionadas(),
                });

            return produto;
        }

        public CriarProduto ObterProdutoComPrecoUnitarioAbaixoDe20()
        {
            var produto = new Faker<CriarProduto>("pt_PT")
                .CustomInstantiator(f => new CriarProduto
                {
                    CategoriaId = Guid.Empty,
                    UnidadeId = Guid.NewGuid(),
                    ImpostoId = Guid.NewGuid(),
                    MotivoId = Guid.NewGuid(),
                    Nome = f.Commerce.ProductName(),
                    Descricao = f.Commerce.ProductDescription(),
                    PrecoUnitario = f.Commerce.Price(1, 19, 2, "KZ").First(),
                    UrlImagemPrincipal = f.Image.LoremPixelUrl(),
                    ImagemUpload = f.Image.LoremPixelUrl(),
                    EmPromocao = true,
                    PrecoPromocional = f.Commerce.Price(20, 2000, 2, "KZ").First(),
                    Destaque = false,
                    NovoLancamento = false,
                    MaisVendido = false,
                    MaisProcurado = false,
                    EmEstoque = false,
                    Activo = true,
                    Observacao = f.Commerce.ProductDescription(),
                    ProdutosRelacionados = ObterProdutosRelacionadas(),
                    ImagensOpcionaisUrls = ObterImagensRelacionadas(),
                });

            return produto;
        }

        private List<string> ObterImagensRelacionadas()
        {
            return new Faker<string>("pt_PT")
                .CustomInstantiator(f => f.Image.LoremPixelUrl()).Generate(2);
        }

        private List<Guid> ObterProdutosRelacionadas()
        {
            return new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        }
    }
}
