namespace MeusLegumes.Infrastructure.Identity.Models;

public class AppUser : IdentityUser<Guid>
{
    public IEnumerable<AppUserRole> UserRoles { get; set; }
}

