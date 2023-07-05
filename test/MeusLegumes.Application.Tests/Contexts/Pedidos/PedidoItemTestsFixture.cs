using Bogus;
using MeusLegumes.Application.Contexts.Pedidos.CommandHandlers;
using MeusLegumes.Application.Contexts.Pedidos.Commands;
using MeusLegumes.Domain.Contexts.Pedidos.Repositories;
using Moq.AutoMock;

namespace MeusLegumes.Application.Tests.Contexts.Pedidos;

[CollectionDefinition(nameof(PedidoItemTestsCollection))]
public class PedidoItemTestsCollection : ICollectionFixture<PedidoItemTestsFixture>
{
}

public class PedidoItemTestsFixture
{
    public AutoMocker Mocker;
    public AdicionarPedidoItemCommandHandler AdicionarItemHandler;
    public AtualizarPedidoItemCommandHandler AtualizarItemHandler;

    public void SetUpHandlers()
    {
        Mocker = new AutoMocker();
        AdicionarItemHandler =  Mocker.CreateInstance<AdicionarPedidoItemCommandHandler>();
        AtualizarItemHandler = Mocker.CreateInstance<AtualizarPedidoItemCommandHandler>();

        SetupIPedidoRepository();
    }

    public AdicionarPedidoItemCommand ObterCommandValido()
    {
        return new Faker<AdicionarPedidoItemCommand>()
            .CustomInstantiator(f => new AdicionarPedidoItemCommand(
                Guid.NewGuid(),
                Guid.NewGuid(),
                f.Commerce.ProductName(),
                2,
                f.Commerce.Price(20, 5000, 2, "AKZ").First()
            ));
    }

    public AdicionarPedidoItemCommand ObterCommandInvalido()
    {
        return new Faker<AdicionarPedidoItemCommand>()
            .CustomInstantiator(f => new AdicionarPedidoItemCommand(
                Guid.NewGuid(),
                Guid.Empty,
                f.Commerce.ProductName(),
                0,
                0
            ));
    }

    public AtualizarPedidoItemCommand ObterAtualizarCommandValido()
    {
        return new AtualizarPedidoItemCommand(Guid.NewGuid(), Guid.NewGuid(), 2);
    }

    public AtualizarPedidoItemCommand ObterAtualizarCommandInValido()
    {
        return new AtualizarPedidoItemCommand(Guid.Empty, Guid.Empty, 0);
    }

    private void SetupIPedidoRepository()
    {
        Mocker.GetMock<IPedidoRepository>().Setup(m => m.UnitOfWork.SaveChangesAsync(CancellationToken.None)).Returns(Task.FromResult(true));
    }
}
