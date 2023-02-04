namespace SobaFw.Shared;

public class BaseLocalizer<TEntity, Resource> : IBaseLocalizer<TEntity, Resource>
    where TEntity : BaseEntity
    where Resource : class
{
    public BaseLocalizer(
        IStringLocalizer<Resource> entityLocalizer,
        IStringLocalizer<SharedResource> sharedLocalizer,
        IStringLocalizer<ValidationResource> validationLocalizer,
        IStringLocalizer<ExceptionResource> exceptionLocalizer)
    {
        EntityLocalizer = entityLocalizer;
        SharedLocalizer = sharedLocalizer;
        ValidationLocalizer = validationLocalizer;
        ExceptionLocalizer = exceptionLocalizer;
    }

    public IStringLocalizer<Resource> EntityLocalizer { get; }
    public IStringLocalizer<SharedResource> SharedLocalizer { get; }
    public IStringLocalizer<ValidationResource> ValidationLocalizer { get; }
    public IStringLocalizer<ExceptionResource> ExceptionLocalizer { get; }
}