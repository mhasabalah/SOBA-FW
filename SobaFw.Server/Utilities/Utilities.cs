namespace SobaFw.Server;
public static class Utilities<TEntity> where TEntity : BaseEntity
{
    public static void ThrowExceptionIfParameterNotSupplied(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException($"{nameof(TEntity)} was not provided.");
    }
    
    public static void ThrowExceptionIfParameterNotSupplied(IEnumerable<TEntity> entities)
    {
        if (entities == null || !entities.Any())
            throw new ArgumentNullException($"{nameof(TEntity)} was not provided.");
    }
}