namespace MeusLegumes.Domain.Contexts.Usuarios.Services;

public interface IJwtService
{
    Task<string> GetJwtString(Usuario usuario);
}
