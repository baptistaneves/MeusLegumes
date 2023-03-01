namespace MeusLegumes.Application.Contexts.Impostos.Services;

public class ImpostoAppService : BaseService, IImpostoAppService
{
    private readonly IImpostoRepository _impostoRepository;
    private readonly IMapper _mapper;
    public ImpostoAppService(INotifier notifier,
                               IMapper mapper,
                               IImpostoRepository impostoRepository) : base(notifier)
    {
        _mapper = mapper;
        _impostoRepository = impostoRepository;
    }

    public async Task Adicionar(CriarImposto imposto, CancellationToken cancellationToken)
    {
        if (!Validate(new CriarImpostoValidation(), imposto)) return;

        if(_impostoRepository.BuscarAsync(c => c.Descricao == imposto.Descricao).Result.Any())
        {
            Notify(ImpostoErrorMessages.ImpostoJaExiste);
            return;
        }

        _impostoRepository.Adicionar(new Imposto(imposto.Descricao, imposto.Taxa, imposto.TipoDeTaxa));
        await _impostoRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task Actualizar(ActualizarImposto imposto, CancellationToken cancellationToken)
    {
        if (!Validate(new ActualizarImpostoValidation(), imposto)) return;

        if(!_impostoRepository.BuscarAsync(c => c.Id == imposto.Id).Result.Any())
        {
            Notify(ImpostoErrorMessages.ImpostoNaoEncontrado);
            return;
        }

        if (_impostoRepository.BuscarAsync(c => c.Descricao == imposto.Descricao && c.Id != imposto.Id).Result.Any())
        {
            Notify(ImpostoErrorMessages.ImpostoJaExiste);
            return;
        }

        _impostoRepository.Actualizar(_mapper.Map<Imposto>(imposto));
        await _impostoRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task Remover(Guid id, CancellationToken cancellationToken)
    {
        if(_impostoRepository.VerificarSeImpostoPossuiProdutosPorId(id).Result.Produtos.Any())
        {
            Notify(ImpostoErrorMessages.ImpostoNaoPodeSerRemovido);
            return;
        }

        _impostoRepository.Remover(id);
        await _impostoRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<Imposto> ObterPorIdAsync(Guid id)
    {
        return await _impostoRepository.ObterPorIdAsync(id);
    }

    public async Task<IEnumerable<Imposto>> ObterTodosAsync()
    {
        return await _impostoRepository.ObterTodosAsync();
    }

    public void Dispose()
    {
        _impostoRepository.Dispose();
    }
}

