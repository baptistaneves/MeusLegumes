namespace MeusLegumes.Application.Contexts.Impostos.Services;

public class MotivoIsencaoIvaAppService : BaseService, IMotivoIsencaoIvaAppService
{
    private readonly IMotivoIsencaoIvaRepository _motivoRepository;
    private readonly IMapper _mapper;
    public MotivoIsencaoIvaAppService(INotifier notifier,
                               IMapper mapper,
                               IMotivoIsencaoIvaRepository motivoRepository) : base(notifier)
    {
        _mapper = mapper;
        _motivoRepository = motivoRepository;
    }

    public async Task Adicionar(CriarMotivoIsencaoIva motivo, CancellationToken cancellationToken)
    {
        if (!Validate(new CriarMotivoIsencaoIvaValidation(), motivo)) return;

        if(_motivoRepository.BuscarAsync(c => c.CodigoInterno == motivo.CodigoInterno).Result.Any())
        {
            Notify(MotivoIsencaoIvaErrorMessages.MotivoJaExiste);
            return;
        }

        _motivoRepository.Adicionar(new MotivoIsencaoIva(motivo.CodigoInterno, motivo.MencaoFactura, motivo.NormaLegalAplicavel, motivo.Motivo));
        await _motivoRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task Actualizar(ActualizarMotivoIsencaoIva motivo, CancellationToken cancellationToken)
    {
        if (!Validate(new ActualizarMotivoIsencaoIvaValidation(), motivo)) return;

        if(!_motivoRepository.BuscarAsync(c => c.Id == motivo.Id).Result.Any())
        {
            Notify(MotivoIsencaoIvaErrorMessages.MotivoNaoEncontrado);
            return;
        }

        if (_motivoRepository.BuscarAsync(c => c.CodigoInterno == motivo.CodigoInterno).Result.Any())
        {
            Notify(MotivoIsencaoIvaErrorMessages.MotivoJaExiste);
            return;
        }

        _motivoRepository.Actualizar(_mapper.Map<MotivoIsencaoIva>(motivo));
        await _motivoRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task Remover(Guid id, CancellationToken cancellationToken)
    {
        if(_motivoRepository.VerificarSeMotivoPossuiProdutosPorId(id).Result.Produtos.Any())
        {
            Notify(MotivoIsencaoIvaErrorMessages.MotivoNaoPodeSerRemovido);
            return;
        }

        _motivoRepository.Remover(id);
        await _motivoRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<MotivoIsencaoIva> ObterPorIdAsync(Guid id)
    {
        return await _motivoRepository.ObterPorIdAsync(id);
    }

    public async Task<IEnumerable<MotivoIsencaoIva>> ObterTodosAsync()
    {
        return await _motivoRepository.ObterTodosAsync();
    }

    public void Dispose()
    {
        _motivoRepository.Dispose();
    }
}

