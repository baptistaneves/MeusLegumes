using Microsoft.AspNetCore.Mvc.Testing;
namespace MeusLegumes.API.Tests.Config;

public class WebAppFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
}

