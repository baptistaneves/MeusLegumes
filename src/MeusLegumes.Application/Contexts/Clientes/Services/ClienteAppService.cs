namespace MeusLegumes.Application.Contexts.Clientes.Services;

public class ClienteAppService : BaseService, IClienteAppService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IMapper _mapper;
    public ClienteAppService(INotifier notifier,
                             IMapper mapper,
                             IClienteRepository clienteRepository) : base(notifier)
    {
        _mapper = mapper;
        _clienteRepository = clienteRepository;
    }

    public async Task Adicionar(CriarCliente cliente, CancellationToken cancellationToken)
    {
        if (!Validate(new CriarClienteValidation(), cliente)) return;

        if(_clienteRepository.BuscarAsync(c => c.Email == cliente.Email).Result.Any())
        {
            Notify(ClientesErrorMessages.ClienteJaExiste);
            return;
        }

        _clienteRepository.Adicionar(_mapper.Map<Cliente>(cliente));
        await _clienteRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task Actualizar(ActualizarCliente cliente, CancellationToken cancellationToken)
    {
        if (!Validate(new ActualizarClienteValidation(), cliente)) return;

        if(!_clienteRepository.BuscarAsync(c => c.Id == cliente.Id).Result.Any())
        {
            Notify(ClientesErrorMessages.ClienteNaoEncontrado);
            return;
        }

        if (_clienteRepository.BuscarAsync(c => c.Email == cliente.Email && c.Id != cliente.Id).Result.Any())
        {
            Notify(ClientesErrorMessages.ClienteJaExiste);
            return;
        }

        _clienteRepository.Actualizar(_mapper.Map<Cliente>(cliente));
        await _clienteRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task Remover(Guid id, CancellationToken cancellationToken)
    {
        _clienteRepository.Remover(id);
        await _clienteRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<Cliente> ObterPorIdAsync(Guid id)
    {
        return await _clienteRepository.ObterPorIdAsync(id);
    }

    public async Task<IEnumerable<Cliente>> ObterTodosAsync()
    {
        return await _clienteRepository.ObterTodosAsync();
    }

    public void Dispose()
    {
        _clienteRepository.Dispose();
    }
}

