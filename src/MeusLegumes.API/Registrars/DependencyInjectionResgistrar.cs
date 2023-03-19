namespace MeusLegumes.API.Registrars;

public class DependencyInjectionResgistrar : IWebApplicationBuilderRegistrar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(Program));

        //ApplicationContext
        builder.Services.AddScoped<ApplicationContext>();

        //Notifications
        builder.Services.AddScoped<INotifier, Notifier>();

        //Jwt Service
        builder.Services.AddScoped<JwtService>();

        //ImageUpload
        builder.Services.AddScoped<IImageUploadService, ImageUploadService>();

        //Categorias
        builder.Services.AddScoped<ICategoriaAppService, CategoriaAppService>();
        builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();

        //Unidades
        builder.Services.AddScoped<IUnidadeAppService, UnidadeAppService>();
        builder.Services.AddScoped<IUnidadeRepository, UnidadeRepository>();

        //Endereços
        builder.Services.AddScoped<IProvinciaAppService, ProvinciaAppService>();
        builder.Services.AddScoped<IProvinciaRepository, ProvinciaRepository>();

        //Impostos
        builder.Services.AddScoped<IImpostoAppService, ImpostoAppService>();
        builder.Services.AddScoped<IImpostoRepository, ImpostoRepository>();

       
        builder.Services.AddScoped<IMotivoIsencaoIvaAppService, MotivoIsencaoIvaAppService>();
        builder.Services.AddScoped<IMotivoIsencaoIvaRepository, MotivoIsencaoIvaRepository>();

        //Clientes
        builder.Services.AddScoped<IClienteAppService, ClienteAppService>();
        builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

        //Produtos
        builder.Services.AddScoped<IProdutoAppService, ProdutoAppService>();
        builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

        //Pacotes
        builder.Services.AddScoped<IPacoteAppService, PacoteAppService>();
        builder.Services.AddScoped<IPacoteRepository, PacoteRepository>();
    }
}
