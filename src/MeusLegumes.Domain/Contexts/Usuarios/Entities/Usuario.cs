namespace MeusLegumes.Domain.Contexts.Usuarios.Entities;

public class Usuario : Entity
{
    public string UserName { get; private set; }
    public string Email { get; private set; }

    public Usuario(string nome, string email)
    {
        UserName = nome;
        Email = email;
    }

    public Usuario() { }
}
