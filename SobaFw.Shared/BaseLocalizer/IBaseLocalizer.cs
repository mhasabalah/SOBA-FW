namespace SobaFw.Shared;

public interface IBaseLocalizer<TEntity, Resource>
    where TEntity : BaseEntity
    where Resource : class
{
    IStringLocalizer<Resource> EntityLocalizer { get; }
    IStringLocalizer<SharedResource> SharedLocalizer { get; }
    IStringLocalizer<ValidationResource> ValidationLocalizer { get; }
    IStringLocalizer<ExceptionResource> ExceptionLocalizer { get; }
}