namespace MeusLegumes.Application.Contexts.Identity.CommandHandlers;

public class LoginCommandHandler : IRequestHandler<LoginCommand, IdentityResponse>
{
    private readonly SignInManager<IdentityUser> _siginManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly INotifier _notifier;
    private readonly JwtService _jwtService;

    public LoginCommandHandler(UserManager<IdentityUser> userManager,
                               SignInManager<IdentityUser> siginManager,
                               INotifier notifier,
                               JwtService jwtService)
    {
        _userManager = userManager;
        _siginManager = siginManager;
        _notifier = notifier;
        _jwtService = jwtService;
    }

    public async Task<IdentityResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var identityUser = await _userManager.FindByEmailAsync(request.Email);
        if (identityUser is null)
        {
            _notifier.Handle(new Notification( IdentityErrorMessages.IncorrectUserName));
            return null;
        }

        var siginResult = await _siginManager.CheckPasswordSignInAsync(identityUser, request.Password, true);

        if (siginResult.IsLockedOut)
        {
            _notifier.Handle(new Notification(IdentityErrorMessages.LockoutOnFailure));
            return null;
        }

        if (!siginResult.Succeeded)
        {
            _notifier.Handle(new Notification(IdentityErrorMessages.IncorrectPassword));
            return null;
        }

        return new IdentityResponse
        {
            Id = identityUser.Id,
            Email = identityUser.Email,
            Nome = identityUser.UserName,
            Token = await _jwtService.GetJwtString(identityUser)
        };
    }
}
