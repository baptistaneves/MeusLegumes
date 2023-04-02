namespace MeusLegumes.Application.Contexts.Identity.Queries;

public class UsuarioQueries : IUsuarioQueries
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioQueries(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<IEnumerable<UsuarioDto>> ObterUsuarios()
    {
        return await _usuarioRepository.ObterTodosUsuarios();
    }
    public async Task<UsuarioDto> ObterUsuarioPorId(Guid id)
    {
        return await _usuarioRepository.ObterUsuarioPorId(id);
    }
}
