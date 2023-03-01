using MeusLegumes.Domain.DomainObjects;

namespace MeusLegumes.Domain.Contexts.Enderecos.Repositories;

public interface IProvinciaRepository : IRepository<Provincia>
{
    Task<Provincia> VerificarSeProvinciaPossuiMunicipiosPorId(Guid id);
    Task<Municipio> VerificarSeMunicipioPossuiClientesPorId(Guid id);

    void AdicionarMunicipio(Municipio municipio);
    void ActualizarMunicipio(Municipio municipio);
    void RemoverMunicipio(Municipio municipio);
    Task<Municipio> ObterMunicipioPorIdAsync(Guid id);
    Task<IEnumerable<Municipio>> ObterMunicipiosAsync();
    Task<IEnumerable<Municipio>> BuscarMunicipioAsync(Expression<Func<Municipio, bool>> predicate);
}
