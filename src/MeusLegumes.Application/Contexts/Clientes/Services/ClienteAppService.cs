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

    public async Task Adicionar(CriarCliente novoCliente, string identityUserId, CancellationToken cancellationToken)
    {
        if (!Validate(new CriarClienteValidation(), novoCliente)) return;

        if(_clienteRepository.BuscarAsync(c => c.Email == novoCliente.Email).Result.Any())
        {
            Notify(ClientesErrorMessages.ClienteJaExiste);
            return;
        }

        var cliente = new Cliente(novoCliente.Nome, identityUserId, novoCliente.Tipo, novoCliente.NumeroContribuinte, novoCliente.TelefonePrincipal, novoCliente.TelefoneAlternativo, novoCliente.Email, novoCliente.MunicipioId, novoCliente.Rua, novoCliente.Casa, novoCliente.CodigoPostal, novoCliente.PontoDeReferencia);
        
        _clienteRepository.Adicionar(cliente);
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

