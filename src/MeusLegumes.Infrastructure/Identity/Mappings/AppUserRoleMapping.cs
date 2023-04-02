namespace MeusLegumes.Infrastructure.Identity.Mappings;

internal class AppUserRoleMapping : IEntityTypeConfiguration<AppUserRole>
{
    public void Configure(EntityTypeBuilder<AppUserRole> builder)
    {

        builder.HasOne(a => a.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(u => u.UserId);

        builder.HasOne(a => a.Role)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(u => u.RoleId);
    }
}

