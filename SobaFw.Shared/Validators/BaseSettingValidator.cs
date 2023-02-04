namespace SobaFw.Shared;

public class BaseSettingsValidator<TEntity, Resource> : BaseValidator<TEntity, Resource>
    where TEntity : BaseSettingsEntity
    where Resource : class
{
    public BaseSettingsValidator(IBaseLocalizer<TEntity, Resource> localizer) : base(localizer)
    {
        RuleFor(e => e.Name).NotEmpty(localizer)!.MaximumLength(500, localizer);
        RuleFor(e => e.NameSecondLanguage).NotEmpty(localizer)!.MaximumLength(500, localizer);
    }
}