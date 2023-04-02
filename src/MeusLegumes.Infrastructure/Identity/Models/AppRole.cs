namespace MeusLegumes.Infrastructure.Identity.Models;

public class AppRole : IdentityRole<Guid>
{
    public IEnumerable<AppUserRole> UserRoles { get; set; }
}

