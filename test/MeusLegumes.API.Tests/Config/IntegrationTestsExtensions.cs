using System.Net.Http.Headers;

namespace MeusLegumes.API.Tests.Config;

public static class IntegrationTestsExtensions
{
    public static void AtribuirToken(this HttpClient client, string token)
    {
        client.AtribuirJsonMediaType();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public static void AtribuirJsonMediaType(this HttpClient client) 
    {
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
}
