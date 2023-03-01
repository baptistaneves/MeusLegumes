using System.Linq;

namespace MeusLegumes.Infrastructure.Repositories.Enderecos;

public class ProvinciaRepository : Repository<Provincia>, IProvinciaRepository
{
    public ProvinciaRepository(ApplicationContext context) : base(context) { }

    public void AdicionarMunicipio(Municipio municipio)
    {
        _context.Municipios.Add(municipio);
    }

    public void ActualizarMunicipio(Municipio municipio)
    {
        _context.Municipios.Update(municipio);
    }

    public void RemoverMunicipio(Municipio municipio)
    {
        _context.Municipios.Remove(municipio);
    }

    public async Task<Municipio> ObterMunicipioPorIdAsync(Guid id)
    {
        return await _context.Municipios.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<IEnumerable<Municipio>> ObterMunicipiosAsync()
    {
        return await _context.Municipios.AsNoTracking().ToListAsync();
    }

    public async Task<Provincia> VerificarSeProvinciaPossuiMunicipiosPorId(Guid id)
    {
        return await _context.Provincias.AsNoTracking().Include(p => p.Municipios).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Municipio> VerificarSeMunicipioPossuiClientesPorId(Guid id)
    {
        return await _context.Municipios.AsNoTracking().Include(p => p.Clientes).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Municipio>> BuscarMunicipioAsync(Expression<Func<Municipio, bool>> predicate)
    {
        return await _context.Municipios.AsNoTracking().Where(predicate).ToListAsync();
    }
}
