using MeusLegumes.Application.Contexts.Identity.Models;

namespace MeusLegumes.Application.Contexts.Identity.Commands;

public class LoginCommand : Command<IdentityResponse>
{
    public string Email { get; private set; }
    public string Password { get; private set; }


    public LoginCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
