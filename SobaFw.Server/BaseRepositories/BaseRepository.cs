namespace SobaFw.Server;

public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    protected DbSet<TEntity> dbSet;
    private readonly ApplicationContext _context;
    public BaseRepository(ApplicationContext context)
    {
        _context = context;
        dbSet = _context.Set<TEntity>();
    }

    public virtual async Task<IEnumerable<TEntity>> Get() => await dbSet.ToListAsync();

    public virtual async Task<TEntity> Get(Guid id) => await dbSet.FirstOrDefaultAsync(e => e.Id == id) ?? Activator.CreateInstance<TEntity>();

    public virtual async Task<TEntity> Add(TEntity entity)
    {
        Utilities<TEntity>.ThrowExceptionIfParameterNotSupplied(entity);
        EntityEntry<TEntity> entityEntry = await dbSet.AddAsync(entity);
        await SaveChangesAsync();

        return entityEntry.Entity;
    }
    public virtual async Task Add(IEnumerable<TEntity> entities)
    {
        Utilities<TEntity>.ThrowExceptionIfParameterNotSupplied(entities);

        await dbSet.AddRangeAsync(entities);
        await SaveChangesAsync();
    }

    public virtual async Task<TEntity> Update(TEntity entity, bool isAutosSaveChangesEnabled = true)
    {
        Utilities<TEntity>.ThrowExceptionIfParameterNotSupplied(entity);
        await ThrowExceptionIfIfEntityExistsInDatabase(entity);

        var entityEntry = await Task.Run(() => dbSet.Update(entity));

        if (isAutosSaveChangesEnabled)
            await SaveChangesAsync();

        return entityEntry.Entity;
    }
    
    public virtual async Task<IEnumerable<TEntity>> Update(IEnumerable<TEntity> entities)
    {
        foreach (TEntity entity in entities)
            await Task.Run(() => dbSet.UpdateRange(entity));

        await SaveChangesAsync();
        return entities;
    }
    

    public virtual async Task<TEntity> Remove(Guid id)
    {
        var entityFromDb = await EntityFromDb(id);

        await Task.Run(() => dbSet.Remove(entityFromDb));
        await SaveChangesAsync();

        return entityFromDb;
    }
    
    public virtual async Task<TEntity> Remove(TEntity entity)
    {
        if (entity is null || entity.Id == null)
            throw new ArgumentNullException($"{nameof(TEntity)} was not provided.");

        var entityFromDb = await EntityFromDb(entity.Id);

        await Task.Run(() => dbSet.Remove(entity));
        await SaveChangesAsync();
        return entityFromDb;
    }
    
    public virtual async Task<IEnumerable<TEntity>> Remove(IEnumerable<TEntity> entities)
    {
        if (entities == null || !entities.Any())
            throw new ArgumentNullException($"{nameof(TEntity)} was not provided.");

        await Task.Run(() => dbSet.RemoveRange(entities));
        await SaveChangesAsync();
        return entities;
    }

    private async Task<TEntity?> EntityFromDb(Guid id)
    {
        TEntity? entityFromDb = await Get(id);
        if (entityFromDb == null)
            throw new ArgumentNullException($"{nameof(TEntity)} was not found in DB");
        return entityFromDb;
    }

    private async Task ThrowExceptionIfIfEntityExistsInDatabase(TEntity entity)
    {
        if (entity.Id == null && entity.Id != Guid.Empty)
            throw new ArgumentNullException($"Id of {typeof(TEntity).Name} is null or has an empty GUID");

        TEntity? entityFromDb = await Get(entity.Id);
        if (entityFromDb == null)
            throw new ArgumentNullException($"{nameof(TEntity)} was not found in DB");
    }

    public async Task<IDbContextTransaction> GetTransaction() => await _context.Database.BeginTransactionAsync();

    protected async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}