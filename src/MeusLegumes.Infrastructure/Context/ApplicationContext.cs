namespace MeusLegumes.Infrastructure.Context;

public class ApplicationContext : IdentityDbContext<AppUser, AppRole, Guid,
                                IdentityUserClaim<Guid>, AppUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>,
                                IdentityUserToken<Guid>>, IUnitOfWork
{
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Unidade> Unidades { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Provincia> Provincias { get; set; }
    public DbSet<Municipio> Municipios { get; set; }
    public DbSet<Imposto> Impostos { get; set; }
    public DbSet<MotivoIsencaoIva> MotivosIsencaoIva { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<ProdutoRelacionado> ProdutoRelacionados { get; set; }
    public DbSet<ProdutoImagem> ProdutoImagens { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<PedidoItem> PedidoItensProduto { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

        modelBuilder.HasSequence<int>("CODIGO_PEDIDO").StartsAt(100).IncrementsBy(1);
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await base.SaveChangesAsync(cancellationToken) > 0;
    }
}
