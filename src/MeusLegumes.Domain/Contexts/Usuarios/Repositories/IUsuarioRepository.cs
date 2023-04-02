namespace MeusLegumes.Domain.Contexts.Usuarios.Repositories;

public interface IUsuarioRepository
{
    Task<CriarUsuarioResponse> Adicionar(Usuario usuario, string password);
    Task<CriarUsuarioResponse> Actualizar(string Nome, string Perfil);
    Task<CriarUsuarioResponse> Remover(Guid id);
    Task<UsuarioDto> ObterUsuarioPorId(Guid id);
    Task<IEnumerable<UsuarioDto>> ObterTodosUsuarios();
    Task<CriarUsuarioResponse> AdicionarAoPerfil(Guid id, string perlfi);
    Task<Usuario> ObterUsuarioPorEmail(string email);
    Task<LoginResponse> CheckPasswordAsync(Guid id, string password);
}
