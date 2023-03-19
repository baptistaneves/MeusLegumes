namespace MeusLegumes.Application.Contexts.Identity.CommandHandlers;

public class CriarUsuarioCommandHandler : IRequestHandler<CriarUsuarioCommand, IdentityResponse>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly INotifier _notifier;
    private readonly JwtService _jwtService;


    public CriarUsuarioCommandHandler(UserManager<IdentityUser> userManager,
                                      INotifier notifier,
                                      JwtService jwtService)
    {
        _userManager = userManager;
        _notifier = notifier;
        _jwtService = jwtService;
    }

    public async Task<IdentityResponse> Handle(CriarUsuarioCommand request, CancellationToken cancellationToken)
    {
        var identityUser = await CriarUsuario(request.Email, request.Password, cancellationToken);
        if (identityUser is null) return null;

        if (!await AdicionarUsuarioAoPerfil(identityUser, request.Perfil)) return null;

        return new IdentityResponse
        {
            Id = identityUser.Id,
            Email = identityUser.Email,
            Nome = identityUser.UserName,
            Token = await _jwtService.GetJwtString(identityUser)
        };
    }

    private async Task<IdentityUser> CriarUsuario(string email, string password, CancellationToken cancellationToken)
    {
        var identity = new IdentityUser { Email = email, UserName = email };

        var result = await _userManager.CreateAsync(identity, password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                _notifier.Handle(new Notification(error.Description));
                return null;
            }

        }

        return identity;
    }

    private async Task<bool> AdicionarUsuarioAoPerfil(IdentityUser identityUser, string perlfil)
    {
        var result = await _userManager.AddToRoleAsync(identityUser, perlfil);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                
                _notifier.Handle(new Notification(error.Description));
                return false;
            }
        }
        return true;
    }

    
}
