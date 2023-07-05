using MeusLegumes.Application.Contexts.Identity.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace MeusLegumes.API.Tests.Config;

public class IntegrationTestsFixtureBase<TProgram> : IDisposable where TProgram : class
{ 
    public HttpClient Client;
	public readonly WebAppFactory<TProgram> Factory;

    public string UsuarioToken;

	public IntegrationTestsFixtureBase()
	{
		var clientOptions = new WebApplicationFactoryClientOptions
		{
			BaseAddress = new Uri("https://localhost:7286")
		};

        Factory = new WebAppFactory<TProgram>();
        Client = Factory.CreateClient(clientOptions);
	}

    public async Task RealizarLogin()
    {
        var login = new Login
        {
            Email = "email@meuslegumes.com",
            Senha = "password",
        };

        var response = await Client.PostAsJsonAsync("api/login", login);
        response.EnsureSuccessStatusCode();

        var identityResponse = await response.Content.ReadFromJsonAsync<IdentityResponse>();
        UsuarioToken = identityResponse.Token;
    }

    public void Dispose()
    {
        Client.Dispose();
        Factory.Dispose();
    }
}