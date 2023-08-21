using MeusLegumes.API.Tests.Config;
using MeusLegumes.Application.Contexts.Pedidos.Commands;

namespace MeusLegumes.API.Tests.Contexts.Pedidos;

[CollectionDefinition(nameof(PedidoIntegrationColletion))]
public class PedidoIntegrationColletion : ICollectionFixture<PedidoIntegrationFixture>
{
}

public class PedidoIntegrationFixture : IntegrationTestsFixtureBase<Program>
{
    private Guid CLIENTE = new Guid("E0D33624-3AE3-48C9-8969-D6FD3FF15161");

    public AdicionarPedidoItemCommand AdicionarNovoItemValido()
    {
        return new AdicionarPedidoItemCommand(CLIENTE, new Guid("82A895AA-5A00-4979-8403-DEC1D2F86428"),
            "Pimentas", 1, 10000);
    }

    public AdicionarPedidoItemCommand AdicionarNovoItemInValido()
    {
        return new AdicionarPedidoItemCommand(Guid.Empty, new Guid("82A895AA-5A00-4979-8403-DEC1D2F86428"), "", 0,0);
    }

    public AdicionarPedidoItemCommand AdicionarNovoItemValidoNoPedidoRascunho()
    {
        return new AdicionarPedidoItemCommand(CLIENTE, new Guid("57B9EC02-C73F-45A3-B9F2-BF38FFEFD4DA"),
            "Pacote Combo", 1, 3000);
    }

    public AdicionarPedidoItemCommand AdicionarNovoItemInValidoNoPedidoRascunho()
    {
        return new AdicionarPedidoItemCommand(CLIENTE, Guid.Empty,
            "Pacote Combo", 0, 0);
    }

    public AtualizarPedidoItemCommand AtualizarItemValido()
    {
        return new AtualizarPedidoItemCommand(CLIENTE, new Guid("82A895AA-5A00-4979-8403-DEC1D2F86428"), 3);
    }

    public AtualizarPedidoItemCommand AtualizarItemInValido()
    {
        return new AtualizarPedidoItemCommand(CLIENTE, Guid.Empty, 0);
    }

    public Guid RemoverItemValido()
    {
        return new Guid("82A895AA-5A00-4979-8403-DEC1D2F86428");
    }

    public Guid RemoverItemInValido()
    {
        return Guid.Empty;
    }
}