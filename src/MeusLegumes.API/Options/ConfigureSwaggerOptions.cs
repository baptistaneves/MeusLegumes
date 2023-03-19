namespace MeusLegumes.API.Options;
public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
        }

        var scheme = GetJwtSecurityScheme();
        options.AddSecurityDefinition(scheme.Reference.Id, scheme);
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {scheme, new string[0] }
        });
    }

    private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
    {
        var info = new OpenApiInfo
        {
            Title = "Meus Legumes",
            Version = description.ApiVersion.ToString(),
            Description = "Sistema Web para comércio de Legumes",
            Contact = new OpenApiContact
            {
                Name = "Baptista Neves",
                Email = "baptistafirminoneves@gmail.com"
            },
            License = new OpenApiLicense
            {
                Name = "CC BY"
            }
        };

        if (description.IsDeprecated)
        {
            info.Description = "Essa versão da API está absoleta";

        }
        return info;
    }

    private OpenApiSecurityScheme GetJwtSecurityScheme()
    {
        return new OpenApiSecurityScheme
        {
            Name = "Autenticação JWT",
            Description = "Copie 'Bearer ' + token'",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Reference = new OpenApiReference
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme
            }
        };
    }

}
