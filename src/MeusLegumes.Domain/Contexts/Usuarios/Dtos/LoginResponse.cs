namespace MeusLegumes.Domain.Contexts.Usuarios.Dtos;

public class LoginResponse
{
    public LoginResponse(bool succeeded, bool isLockedOut)
    {
        Succeeded = succeeded;
        IsLockedOut = isLockedOut;  
    }

    public bool Succeeded { get; private set; }
    public bool IsLockedOut { get; private set; }
    
}
