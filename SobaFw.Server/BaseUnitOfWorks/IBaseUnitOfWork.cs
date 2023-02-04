namespace SobaFw.Server;

public interface IBaseUnitOfWork<TEntity> : IDisposable
    where TEntity : BaseEntity
{
    Task<TEntity> Create(TEntity entity);
    Task<IEnumerable<TEntity>> Create(IEnumerable<TEntity> entities);

    Task<IEnumerable<TEntity>> Read();
    Task<TEntity> Read(Guid id);

    Task<TEntity> Update(TEntity entity);
    Task<IEnumerable<TEntity>> Update(IEnumerable<TEntity> entities);

    Task<TEntity> Delete(Guid id);
    Task<TEntity> Delete(TEntity entity);
    Task<IEnumerable<TEntity>> Delete(IEnumerable<TEntity> entities);

}