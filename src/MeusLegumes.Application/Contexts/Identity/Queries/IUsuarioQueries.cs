namespace MeusLegumes.Application.Contexts.Identity.Queries;

public interface IUsuarioQueries
{
    Task<IEnumerable<UsuarioDto>> ObterUsuarios();
    Task<UsuarioDto> ObterUsuarioPorId(Guid id);
}
