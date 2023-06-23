using AutoMapper;
using Bogus;
using MeusLegumes.Application.Contexts.Unidades.Contracts;
using MeusLegumes.Application.Contexts.Unidades.Services;
using MeusLegumes.Domain.Communication.Notifications;
using MeusLegumes.Domain.Contexts.Unidades.Repositories;
using Moq;

namespace MeusLegumes.Application.Tests.Contexts.Unidades;

public class UnidadeAppServiceTests
{
    [Fact(DisplayName = "Adicionar unidade com sucesso")]
    [Trait("Unidade", "Unidade Service Tests")]
    public async Task UnidadeService_Adicionar_UnidadeAdicionadaComSucesso()
    {
        //Arrage
        var unidade = new Faker<CriarUnidade>("pt_PT")
            .CustomInstantiator(f => new CriarUnidade { Descricao = f.Lorem.Letter(4) });

        var notifier = new Mock<INotifier>();
        var unidadeRepository = new Mock<IUnidadeRepository>();
        var mapper = new Mock<IMapper>();

        unidadeRepository.Setup(r => r.UnitOfWork.SaveChangesAsync(CancellationToken.None)).Returns(Task.FromResult(true));
        var unidadeAppService = new UnidadeAppService(notifier.Object, unidadeRepository.Object, mapper.Object);

        //Act
        await unidadeAppService.Adicionar(unidade, CancellationToken.None);

        //Assert
        unidadeRepository.Verify(v => v.UnitOfWork.SaveChangesAsync(CancellationToken.None), Times.Once);
    }

    [Fact(DisplayName = "Adicionar unidade com erro")]
    [Trait("Unidade", "Unidade Service Tests")]
    public async Task UnidadeService_Adicionar_UnidadeAdicionadaComDescricaoInvalida()
    {
        //Arrange
        var notifier = new Mock<INotifier>();
        var mapper = new Mock<IMapper>();
        var unidadeRepository = new Mock<IUnidadeRepository>();

        unidadeRepository.Setup(r => r.UnitOfWork.SaveChangesAsync(CancellationToken.None)).Returns(Task.FromResult(true));
        var unidadeAppService = new UnidadeAppService(notifier.Object, unidadeRepository.Object, mapper.Object);

        var unidade = new Faker<CriarUnidade>()
            .CustomInstantiator(r => new CriarUnidade { Descricao = r.Lorem.Letter(2) });

        //Act 
        await unidadeAppService.Adicionar(unidade, CancellationToken.None);

        //Assert
        unidadeRepository.Verify(v => v.UnitOfWork.SaveChangesAsync(CancellationToken.None), Times.Never);
    }

    [Fact(DisplayName = "Adicionar unidade com descrição vázia")]
    [Trait("Unidade", "Unidade Service Tests")]
    public async Task UnidadeService_Adicionar_UnidadeAdicionadaComDescricaoVazia()
    {
        //Arrange
        var notifier = new Mock<INotifier>();
        var mapper = new Mock<IMapper>();
        var unidadeRepository = new Mock<IUnidadeRepository>();

        unidadeRepository.Setup(r => r.UnitOfWork.SaveChangesAsync(CancellationToken.None)).Returns(Task.FromResult(true));
        var unidadeAppService = new UnidadeAppService(notifier.Object, unidadeRepository.Object, mapper.Object);

        var unidade = new CriarUnidade { Descricao = ""};

        //Act 
        await unidadeAppService.Adicionar(unidade, CancellationToken.None);

        //Assert
        unidadeRepository.Verify(v => v.UnitOfWork.SaveChangesAsync(CancellationToken.None), Times.Never);
    }
}
