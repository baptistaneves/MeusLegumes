namespace MeusLegumes.Domain.Data;
public interface IRepository<TEntity> : IDisposable where TEntity : Entity
{
    IUnitOfWork UnitOfWork { get; }
    void Adicionar(TEntity entity);
    void Actualizar(TEntity entity);
    void Remover(Guid id);
    Task<TEntity> ObterPorIdAsync(Guid id);
    Task<IEnumerable<TEntity>> ObterTodosAsync();
    Task<IEnumerable<TEntity>> BuscarAsync(Expression<Func<TEntity, bool>> predicate);
}
