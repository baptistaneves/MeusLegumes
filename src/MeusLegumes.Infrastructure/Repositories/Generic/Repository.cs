namespace MeusLegumes.Infrastructure.Repositories.Generic;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
{
    protected readonly ApplicationContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(ApplicationContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public IUnitOfWork UnitOfWork => _context;

    public void Adicionar(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public void Actualizar(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public void Remover(Guid id)
    {
        _dbSet.Remove(new TEntity { Id = id });
    }

    public async Task<TEntity> ObterPorIdAsync(Guid id)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<IEnumerable<TEntity>> ObterTodosAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> BuscarAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
    }

    public void Dispose()
    {
        _context?.Dispose();
    }

}
