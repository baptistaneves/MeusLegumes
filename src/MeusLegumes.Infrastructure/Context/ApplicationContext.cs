namespace MeusLegumes.Infrastructure.Context;

public class ApplicationContext : DbContext, IUnitOfWork
{
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Unidade> Unidades { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Provincia> Provincias { get; set; }
    public DbSet<Municipio> Municipios { get; set; }
    public DbSet<Imposto> Impostos { get; set; }
    public DbSet<MotivoIsencaoIva> MotivosIsencaoIva { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Pacote> Pacotes { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await base.SaveChangesAsync(cancellationToken) > 0;
    }
}
