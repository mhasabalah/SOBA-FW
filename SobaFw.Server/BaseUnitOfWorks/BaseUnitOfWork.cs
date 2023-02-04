namespace SobaFw.Server;

public class BaseUnitOfWork<TEntity> : IBaseUnitOfWork<TEntity>
    where TEntity : BaseEntity
{
    private readonly IBaseRepository<TEntity> _repository;
    private readonly IValidator<TEntity> _validator;
    public BaseUnitOfWork(IBaseRepository<TEntity> repository, IValidator<TEntity> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public virtual async Task<TEntity> Create(TEntity entity)
    {

        using IDbContextTransaction transaction = await _repository.GetTransaction();
        try
        {
            await ThrowIfNotValid(entity);

            entity = await _repository.Add(entity);
            await transaction.CommitAsync();
        }
        catch (Exception exception)
        {
            Log.Error(exception.Message);
            transaction.Rollback();
        }
        return entity;
    }

    public virtual async Task<IEnumerable<TEntity>> Create(IEnumerable<TEntity> entities)
    {
        using IDbContextTransaction transaction = await _repository.GetTransaction();
        try
        {
            await _repository.Add(entities);
            await transaction.CommitAsync();
        }
        catch (Exception exception)
        {
            Log.Error(exception.Message);
            transaction.Rollback();

        }
        return entities;
    }

    public virtual async Task<IEnumerable<TEntity>> Read() => await _repository.Get();
    public virtual async Task<TEntity> Read(Guid id) => await _repository.Get(id);

    public virtual async Task<TEntity> Update(TEntity entity)
    {
        using IDbContextTransaction transaction = await _repository.GetTransaction();
        try
        {
            await ThrowIfNotValid(entity);

            entity = await _repository.Update(entity);
            await transaction.CommitAsync();
        }
        catch (Exception exception)
        {
            Log.Error(exception.Message);
            transaction.Rollback();
        }
        return entity;
    }
    public virtual async Task<IEnumerable<TEntity>> Update(IEnumerable<TEntity> entities)
    {
        using IDbContextTransaction transaction = await _repository.GetTransaction();
        try
        {
            entities = await _repository.Update(entities);
            await transaction.CommitAsync();
        }
        catch (Exception exception)
        {
            Log.Error(exception.Message);
            transaction.Rollback();
        }

        return entities;
    }

    public virtual async Task<TEntity> Delete(Guid id)
    {
        using IDbContextTransaction transaction = await _repository.GetTransaction();
        TEntity entity = Activator.CreateInstance<TEntity>();
        try
        {
            entity = await Read(id);
            await _repository.Remove(id);
            await transaction.CommitAsync();
        }
        catch (Exception exception)
        {
            Log.Error(exception.Message);
            transaction.Rollback();
        }

        return entity;
    }
    public virtual async Task<TEntity> Delete(TEntity entity)
    {
        using IDbContextTransaction transaction = await _repository.GetTransaction();
        try
        {
            entity = await _repository.Remove(entity);
            await transaction.CommitAsync();
        }
        catch (Exception exception)
        {
            Log.Error(exception.Message);
            transaction.Rollback();
        }
        return entity;
    }

    public async Task<IEnumerable<TEntity>> Delete(IEnumerable<TEntity> entities)
    {
        using IDbContextTransaction transaction = await _repository.GetTransaction();
        try
        {
            entities = await _repository.Remove(entities);
            await transaction.CommitAsync();
        }
        catch (Exception exception)
        {
            Log.Error(exception.Message);
            transaction.Rollback();
        }

        return entities;
    }
    private async Task ThrowIfNotValid(TEntity entity)
    {
        ValidationResult result = await _validator.ValidateAsync(entity);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);
    }

    public void Dispose() => _repository.Dispose();
}