namespace MeusLegumes.Application.Contexts.Pacotes.Services;

public class PacoteAppService : BaseService, IPacoteAppService
{
    private readonly IPacoteRepository _pacoteRepository;
    private readonly IMapper _mapper;
    public PacoteAppService(INotifier notifier, 
                            IPacoteRepository pacoteRepository, 
                            IMapper mapper) : base(notifier)
    {
        _pacoteRepository = pacoteRepository;
        _mapper = mapper;
    }
    public async Task Adicionar(CriarPacote pacote, CancellationToken cancellationToken)
    {
        if (!Validate(new CriarPacoteValidation(), pacote)) return;

        if (_pacoteRepository.BuscarAsync(c => c.Nome == pacote.Nome).Result.Any())
        {
            Notify(PacoteErrorMessages.PacoteJaExiste);
            return;
        }

        var novoPacote = _mapper.Map<Pacote>(pacote);

        if(pacote.PacoteProdutos.Any())
        {
            foreach (var pacoteProduto in pacote.PacoteProdutos)
            {
                novoPacote.AdicionarPacoteProduto(new PacoteProduto(pacoteProduto.ProdutoId, pacoteProduto.UnidadeId, pacoteProduto.Quantidade));
            }
        }

        _pacoteRepository.Adicionar(novoPacote);
        await _pacoteRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task Actualizar(ActualizarPacote pacoteActualizado, CancellationToken cancellationToken)
    {
        if (!Validate(new ActualizarPacoteValidation(), pacoteActualizado)) return;

        if (!_pacoteRepository.BuscarAsync(p => p.Id == pacoteActualizado.Id).Result.Any())
        {
            Notify(PacoteErrorMessages.PacoteNaoEncotrado);
            return;
        }

        if (_pacoteRepository.BuscarAsync(c => c.Nome == pacoteActualizado.Nome && c.Id != pacoteActualizado.Id).Result.Any())
        {
            Notify(PacoteErrorMessages.PacoteJaExiste);
            return;
        }

        var pacote = _mapper.Map<Pacote>(pacoteActualizado);

        if (pacoteActualizado.PacoteProdutos.Any())
        {
            foreach (var pacoteProduto in pacoteActualizado.PacoteProdutos)
            {
                pacote.AdicionarPacoteProduto(new PacoteProduto(pacoteProduto.ProdutoId, pacoteProduto.UnidadeId, pacoteProduto.Quantidade));
            }
        }

        _pacoteRepository.Actualizar(pacote);
        await _pacoteRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task Remover(Guid id, CancellationToken cancellationToken)
    {
        _pacoteRepository.Remover(id);
        await _pacoteRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<Pacote> ObterPorIdAsync(Guid id)
    {
        return await _pacoteRepository.ObterPorIdAsync(id);
    }

    public async Task<IEnumerable<Pacote>> ObterTodosAsync()
    {
        return await _pacoteRepository.ObterTodosAsync();
    }

    public void Dispose()
    {
        _pacoteRepository?.Dispose();
    }
}
