namespace SobaFw.Server;

public interface IBaseRepository<TEntity> : IDisposable where TEntity : BaseEntity
{
    Task<TEntity> Add(TEntity entity);
    Task Add(IEnumerable<TEntity> entities);

    Task<IEnumerable<TEntity>> Get();
    Task<TEntity> Get(Guid id);

    Task<TEntity> Update(TEntity entity, bool isAutosSaveChangesEnabled = true);
    Task<IEnumerable<TEntity>> Update(IEnumerable<TEntity> entities);

    Task<TEntity> Remove(Guid id);
    Task<TEntity> Remove(TEntity entity);
    Task<IEnumerable<TEntity>> Remove(IEnumerable<TEntity> entities);

    Task<IDbContextTransaction> GetTransaction();
}