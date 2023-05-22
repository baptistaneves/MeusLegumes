﻿namespace MeusLegumes.API.Registrars;

public class McvWebAppRegistrar : IWebApplicationRegistrar
{
    public void RegisterPipelineComponents(WebApplication app)
    {
        app.ConfigureExceptionHandler(app.Environment);

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                    description.ApiVersion.ToString());
            }
        });

        app.UseHttpsRedirection();

        app.UseCors(policy => {
            policy.AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins("http://localhost:4200");
        });

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
    }
}
