namespace MeusLegumes.API.Registrars;

public class DbResgistrar : IWebApplicationBuilderRegistrar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));

        builder.Services.AddIdentityCore<AppUser>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.User.AllowedUserNameCharacters = string.Empty; 
        })
        .AddErrorDescriber<IdentityMensagensPortugues>()
        .AddSignInManager<SignInManager<AppUser>>()
        .AddRoles<AppRole>()
        .AddRoleManager<RoleManager<AppRole>>()
        .AddEntityFrameworkStores<ApplicationContext>();
    }
}
