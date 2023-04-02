using MeusLegumes.Infrastructure.Identity.AutoMapper;

namespace MeusLegumes.API.Registrars;

public class InfrastructureLayerRegistrar : IWebApplicationBuilderRegistrar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(IdentityMappingProfile));
    }
}

