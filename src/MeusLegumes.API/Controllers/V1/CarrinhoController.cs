namespace MeusLegumes.API.Controllers.V1;

[ApiVersion("1.0")]
[Route(ApiRoutes.BaseRoute)]
public class CarrinhoController : BaseController
{
    private readonly IProdutoAppService _produtoAppService;
    private readonly IPedidoQueries _pedidoQueries;
    private readonly IClienteAppService _clienteAppService;
    private readonly IMediator _mediator;
    public CarrinhoController(INotifier notifier,
                              IProdutoAppService produtoAppService,
                              IPedidoQueries pedidoQueries, IMediator mediator, 
                              IClienteAppService clienteAppService) : base(notifier)
    {
        _produtoAppService = produtoAppService;
        _pedidoQueries = pedidoQueries;
        _mediator = mediator;
        _clienteAppService = clienteAppService;
    }

    [HttpGet(ApiRoutes.Pedido.MeuCarrinho)]
    public async Task<ActionResult> MeuCarrinho()
    {
        return Ok(await ObterCarrinhoCliente());
    }

    [HttpGet(ApiRoutes.Pedido.ResumoDaCompra)]
    public async Task<ActionResult> ResumoDaCompra()
    {
        return Ok(await ObterCarrinhoCliente());
    }

    [HttpPost(ApiRoutes.Pedido.AdicionarItemNoCarrinho)]
    public async Task<ActionResult> AdicionarItemNoCarrinho(Guid produtoId, int quantidade, CancellationToken cancellationToken)
    {
        var produto = await ObterProduto(produtoId);

        if(produto is null) return Response();

        var cliente = await _clienteAppService.OterClientePorUserIdentityId(HttpContext.ObterIdentityUserId());

        var command = new AdicionarPedidoItemCommand(cliente.Id, produto.Id, produto.Nome, quantidade, produto.PrecoUnitario);
        await _mediator.Send(command, cancellationToken);

        return Response();
    }

    [HttpDelete(ApiRoutes.Pedido.RemoverItemDoCarrinho)]
    public async Task<ActionResult> RemoverItemDoCarrinho(Guid produtoId, CancellationToken cancellationToken)
    {
        var produto = await ObterProduto(produtoId);

        if (produto is null) return Response();

        var cliente = await _clienteAppService.OterClientePorUserIdentityId(HttpContext.ObterIdentityUserId());

        var command = new RemoverPedidoItemCommand(cliente.Id, produto.Id);
        await _mediator.Send(command, cancellationToken);

        return Response();
    }

    [HttpPut(ApiRoutes.Pedido.ActualizarItemNoCarrinho)]
    public async Task<ActionResult> ActualizarItemNoCarrinho(Guid produtoId, int quantidade, CancellationToken cancellationToken)
    {
        var produto = await ObterProduto(produtoId);

        if (produto is null) return Response();

        var cliente = await _clienteAppService.OterClientePorUserIdentityId(HttpContext.ObterIdentityUserId());

        var command = new AtualizarPedidoItemCommand(cliente.Id, produto.Id, quantidade);
        await _mediator.Send(command, cancellationToken);

        return Response();
    }

    [HttpPost(ApiRoutes.Pedido.IniciarPedido)]
    public async Task<ActionResult> IniciarPedido([FromBody] CarrinhoViewModel carrinhoViewModel, CancellationToken cancellationToken)
    {
        var carrinho = await ObterCarrinhoCliente();

        var command = new IniciarPedidoCommand(carrinho.PedidoId, carrinho.ClienteId, carrinho.ValorTotal);
        await _mediator.Send(command, cancellationToken);

        return Response();
    }


    [HttpPost(ApiRoutes.Pedido.FinalizarPedido)]
    public async Task<ActionResult> FinalizarPedido(CancellationToken cancellationToken)
    {
        var carrinho = await ObterCarrinhoCliente();

        var command = new FinalizarPedidoCommand(carrinho.PedidoId, carrinho.ClienteId);

        await _mediator.Send(command, cancellationToken);

        return Response();
    }

    private async Task<CarrinhoViewModel> ObterCarrinhoCliente()
    {
        var cliente = await _clienteAppService.OterClientePorUserIdentityId(HttpContext.ObterIdentityUserId());
        return await _pedidoQueries.ObterCarrinhoCliente(cliente.Id);
    }

    private async Task<Produto> ObterProduto(Guid produtoId)
    {
        var produto = await _produtoAppService.ObterPorIdAsync(produtoId);

        if (produto is null)
        {
            Notify(ProdutoErrorMessages.ProdutoNaoEncotrado);
            return null;
        }

        return produto;
    }
}
