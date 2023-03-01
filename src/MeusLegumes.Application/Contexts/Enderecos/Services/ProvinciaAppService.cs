namespace MeusLegumes.Application.Contexts.Enderecos.Services;

public class ProvinciaAppService : BaseService, IProvinciaAppService
{
    private readonly IProvinciaRepository _provinciaRepository;
    private readonly IMapper _mapper;
    public ProvinciaAppService(INotifier notifier,
                             IMapper mapper,
                             IProvinciaRepository provinciaRepository) : base(notifier)
    {
        _mapper = mapper;
        _provinciaRepository = provinciaRepository;
    }
    public async Task Adicionar(CriarProvincia provincia, CancellationToken cancellationToken)
    {
        if (!Validate(new CriarProvinciaValidation(), provincia)) return;

        if (_provinciaRepository.BuscarAsync(u => u.Nome == provincia.Nome).Result.Any())
        {
            Notify(EnderecosErrorMessages.ProvinciaJaExiste);
            return;
        }

        _provinciaRepository.Adicionar(new Provincia(provincia.Nome));
        await _provinciaRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task Actualizar(ActualizarProvincia provincia, CancellationToken cancellationToken)
    {
        if (!Validate(new ActualizarProvinciaValidation(), provincia)) return;

        if (!_provinciaRepository.BuscarAsync(u => u.Id == provincia.Id).Result.Any())
        {
            Notify(EnderecosErrorMessages.ProvinciaNaoEncontrada);
            return;
        }

        if (_provinciaRepository.BuscarAsync(u => u.Nome == provincia.Nome && u.Id != provincia.Id).Result.Any())
        {
            Notify(EnderecosErrorMessages.ProvinciaJaExiste);
            return;
        }

        _provinciaRepository.Actualizar(_mapper.Map<Provincia>(provincia));
        await _provinciaRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
    public async Task Remover(Guid id, CancellationToken cancellationToken)
    {
        if (_provinciaRepository.VerificarSeProvinciaPossuiMunicipiosPorId(id).Result.Municipios.Any())
        {
            Notify(EnderecosErrorMessages.ProvinciaNaoPodeSerRemovida);
            return;
        }

        _provinciaRepository.Remover(id);
        await _provinciaRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<Provincia> ObterPorIdAsync(Guid id)
    {
        return await _provinciaRepository.ObterPorIdAsync(id);
    }

    public async Task<IEnumerable<Provincia>> ObterTodosAsync()
    {
        return await _provinciaRepository.ObterTodosAsync();
    }
    
    public async Task AdicionarMunicipio(CriarMunicipio municipio, CancellationToken cancellationToken)
    {
        if (!Validate(new CriarMunicipioValidation(), municipio)) return;

        if (_provinciaRepository.BuscarMunicipioAsync(u => u.Nome == municipio.Nome).Result.Any())
        {
            Notify(EnderecosErrorMessages.MunicipioJaExiste);
            return;
        }

        _provinciaRepository.AdicionarMunicipio(new Municipio(municipio.ProvinciaId, municipio.Nome));
        await _provinciaRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task ActualizarMunicipio(ActualizarMunicipio municipio, CancellationToken cancellationToken)
    {
        if (!Validate(new ActualizarMunicipioValidation(), municipio)) return;

        if (!_provinciaRepository.BuscarMunicipioAsync(u => u.Id == municipio.Id).Result.Any())
        {
            Notify(EnderecosErrorMessages.MunicipioNaoEncontrada);
            return;
        }

        if (_provinciaRepository.BuscarMunicipioAsync(u => u.Nome == municipio.Nome && u.Id != municipio.Id).Result.Any())
        {
            Notify(EnderecosErrorMessages.MunicipioJaExiste);
            return;
        }

        _provinciaRepository.ActualizarMunicipio(_mapper.Map<Municipio>(municipio));
        await _provinciaRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoverMunicipio(Municipio municipio, CancellationToken cancellationToken)
    {
        if (_provinciaRepository.VerificarSeMunicipioPossuiClientesPorId(municipio.Id).Result.Clientes.Any())
        {
            Notify(EnderecosErrorMessages.MunicipioNaoPodeSerRemovido);
            return;
        }

        _provinciaRepository.RemoverMunicipio(municipio);
        await _provinciaRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<Municipio> ObterMunicipioPorIdAsync(Guid id)
    {
        return await _provinciaRepository.ObterMunicipioPorIdAsync(id);
    }

    public async Task<IEnumerable<Municipio>> ObterMunicipiosAsync()
    {
        return await _provinciaRepository.ObterMunicipiosAsync();
    }
    public void Dispose()
    {
        _provinciaRepository?.Dispose();
    }

}

