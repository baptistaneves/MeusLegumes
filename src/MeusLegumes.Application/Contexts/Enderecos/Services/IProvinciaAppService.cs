namespace MeusLegumes.Application.Contexts.Enderecos.Services;

public interface IProvinciaAppService : IDisposable
{
    Task Adicionar(CriarProvincia provincia, CancellationToken cancellationToken);
    Task Actualizar(ActualizarProvincia unidade, CancellationToken cancellationToken);
    Task Remover(Guid id, CancellationToken cancellationToken);
    Task<Provincia> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Provincia>> ObterTodosAsync();

    Task AdicionarMunicipio(CriarMunicipio municipio, CancellationToken cancellationToken);
    Task ActualizarMunicipio(ActualizarMunicipio municipio, CancellationToken cancellationToken);
    Task RemoverMunicipio(Municipio municipio, CancellationToken cancellationToken);
    Task<Municipio> ObterMunicipioPorIdAsync(Guid id);
    Task<IEnumerable<Municipio>> ObterMunicipiosAsync();
}

