namespace MeusLegumes.Application.Contexts.Unidades.Services;

public class UnidadeAppService : BaseService, IUnidadeAppService
{
    private readonly IUnidadeRepository _unidadeRepository;
    private readonly IMapper _mapper;
    public UnidadeAppService(INotifier notifier,
                             IUnidadeRepository unidadeRepository,
                             IMapper mapper) : base(notifier)
    {
        _unidadeRepository = unidadeRepository;
        _mapper = mapper;
    }

    public async Task Adicionar(CriarUnidade unidade, CancellationToken cancellationToken)
    {
        if (!Validate(new CriarUnidadeValidation(), unidade)) return;

        if (_unidadeRepository.BuscarAsync(u => u.Descricao == unidade.Descricao).Result.Any())
        {
            Notify(UnidadeErrorMessages.UnidadeJaExiste);
            return;
        }

        _unidadeRepository.Adicionar(new Unidade(unidade.Descricao));
        await _unidadeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task Actualizar(ActualizarUnidade unidade, CancellationToken cancellationToken)
    {
        if (!Validate(new ActualizarUnidadeValidation(), unidade)) return;

        if (!_unidadeRepository.BuscarAsync(u => u.Id == unidade.Id).Result.Any())
        {
            Notify(UnidadeErrorMessages.UnidadeNaoEncontrada);
            return;
        }

        if (_unidadeRepository.BuscarAsync(u => u.Descricao == unidade.Descricao && u.Id != unidade.Id).Result.Any())
        {
            Notify(UnidadeErrorMessages.UnidadeJaExiste);
            return;
        }

        _unidadeRepository.Actualizar(_mapper.Map<Unidade>(unidade));
        await _unidadeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task Remover(Guid id, CancellationToken cancellationToken)
    {
        if (_unidadeRepository.VerificarSeUnidadePossuiProdutosPorId(id).Result.Produtos.Any())
        {
            Notify(UnidadeErrorMessages.UnidadeNaoPodeSerRemovida);
            return;
        }

        _unidadeRepository.Remover(id);
        await _unidadeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<Unidade> ObterPorIdAsync(Guid id)
    {
        return await _unidadeRepository.ObterPorIdAsync(id);
    }

    public async Task<IEnumerable<Unidade>> ObterTodosAsync()
    {
        return await _unidadeRepository.ObterTodosAsync();
    }

    public void Dispose()
    {
        _unidadeRepository?.Dispose();
    }
}

