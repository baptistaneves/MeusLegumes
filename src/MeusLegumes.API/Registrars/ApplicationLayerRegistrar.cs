using MeusLegumes.Application.Extensions;

namespace MeusLegumes.API.Registrars;

public class ApplicationLayerRegistrar : IWebApplicationBuilderRegistrar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(RequestsToEntitiesProfile));
        builder.Services.ConfigureApplication(builder.Configuration);
    }
}

