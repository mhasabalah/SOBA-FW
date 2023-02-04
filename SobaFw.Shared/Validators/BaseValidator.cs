namespace SobaFw.Shared;

public class BaseValidator<TEntity, Resource> : AbstractValidator<TEntity>, IValidator<TEntity>, IValidator
    where TEntity : BaseEntity
    where Resource : class
{
    public BaseValidator(IBaseLocalizer<TEntity, Resource> localizer) { }
}