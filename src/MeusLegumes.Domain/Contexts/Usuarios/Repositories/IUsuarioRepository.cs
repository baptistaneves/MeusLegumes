namespace MeusLegumes.Domain.Contexts.Usuarios.Repositories;

public interface IUsuarioRepository
{
    Task<CriarUsuarioResponse> Adicionar(Usuario usuario, string password);
    Task<CriarUsuarioResponse> Actualizar(Usuario usuario);
    Task<CriarUsuarioResponse> Remover(Guid id);
    Task<UsuarioDto> ObterUsuarioPorId(Guid id);
    Task<IEnumerable<UsuarioDto>> ObterTodosUsuarios();
    Task<CriarUsuarioResponse> AdicionarAoPerfil(Guid id, string perlfi);
    Task<CriarUsuarioResponse> RemoverUsuarioDoPerfil(Guid id, string perlfi);
    Task<Usuario> ObterUsuarioPorEmail(string email);
    Task<LoginResponse> CheckPasswordAsync(Guid id, string password);
    Task<CriarUsuarioResponse> AlterarSenha(Guid id, string senhaActual, string novaSenha);
}
