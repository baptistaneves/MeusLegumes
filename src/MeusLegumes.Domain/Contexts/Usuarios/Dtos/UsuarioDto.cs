namespace MeusLegumes.Domain.Contexts.Usuarios.Dtos;

public class UsuarioDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Perfil { get; set; }
}
