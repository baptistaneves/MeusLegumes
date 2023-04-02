namespace MeusLegumes.Infrastructure.Identity.Models;

public class AppUserRole : IdentityUserRole<Guid>
{
    public AppRole Role { get; set; }
    public AppUser User { get; set; }
}

