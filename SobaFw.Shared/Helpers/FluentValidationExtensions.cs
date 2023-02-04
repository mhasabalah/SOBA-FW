namespace SobaFw.Shared;

public static class FluentValidationExtensions
{
    public static IRuleBuilderOptions<TEntity, TProperty> NotEmpty<TEntity, TProperty, Resource>(
      this IRuleBuilder<TEntity, TProperty> ruleBuilder,
      IBaseLocalizer<TEntity, Resource> localizer)
      where TEntity : BaseEntity
      where Resource : class
    {
        string? propertyName = ruleBuilder.GetPropertyName();
        string? loczlizedPropertyName = GetLoczlizedPropertyName(localizer, propertyName);
        IRuleBuilderOptions<TEntity, TProperty> iruleBuilderOptions = DefaultValidatorExtensions.NotEmpty(ruleBuilder);
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
        interpolatedStringHandler.AppendFormatted(loczlizedPropertyName);
        interpolatedStringHandler.AppendLiteral(" ");
        interpolatedStringHandler.AppendFormatted<LocalizedString>(localizer.ValidationLocalizer["IsRequired"]);
        string stringAndClear = interpolatedStringHandler.ToStringAndClear();
        return DefaultValidatorOptions.WithMessage(iruleBuilderOptions, stringAndClear);
    }

    public static IRuleBuilderOptions<TEntity, string> MaximumLength<TEntity, Resource>(
      this IRuleBuilder<TEntity, string> ruleBuilder,
      int maximumLength,
      IBaseLocalizer<TEntity, Resource> localizer)
      where TEntity : BaseEntity
      where Resource : class
    {
        string? propertyName = ruleBuilder.GetPropertyName();
        string? loczlizedPropertyName = GetLoczlizedPropertyName(localizer, propertyName);
        return DefaultValidatorOptions.WithMessage<TEntity, string>(DefaultValidatorExtensions.MaximumLength(ruleBuilder, maximumLength), loczlizedPropertyName + " " + string.Format(localizer.ValidationLocalizer[nameof(MaximumLength)].Value, (object)maximumLength));
    }

    public static IRuleBuilderOptions<TEntity, TProperty> GreaterThan<TEntity, TProperty, Resource>(
      this IRuleBuilder<TEntity, TProperty> ruleBuilder,
      Expression<Func<TEntity, TProperty>> expression,
      IBaseLocalizer<TEntity, Resource> localizer)
      where TEntity : BaseEntity
      where TProperty : IComparable<TProperty>, IComparable
      where Resource : class
    {
        string? propertyName = ruleBuilder.GetPropertyName();
        string? name = (((MemberExpression) expression.Body).Member as PropertyInfo)?.Name;
        string? loczlizedPropertyName1 = GetLoczlizedPropertyName(localizer, propertyName);
        string? loczlizedPropertyName2 = GetLoczlizedPropertyName(localizer, name);
        return DefaultValidatorOptions.WithMessage(DefaultValidatorExtensions.GreaterThan(ruleBuilder, expression), loczlizedPropertyName1 + " " + string.Format(localizer.ValidationLocalizer[nameof(GreaterThan)].Value, loczlizedPropertyName2));
    }

    public static IRuleBuilderOptions<TEntity, TProperty> GreaterThanOrEqualTo<TEntity, TProperty, Resource>(
      this IRuleBuilder<TEntity, TProperty> ruleBuilder,
      Expression<Func<TEntity, TProperty>> expression,
      IBaseLocalizer<TEntity, Resource> localizer)
      where TEntity : BaseEntity
      where TProperty : IComparable<TProperty>, IComparable
      where Resource : class
    {
        string? propertyName = ruleBuilder.GetPropertyName();
        string? name = (((MemberExpression) expression.Body).Member as PropertyInfo)?.Name;
        string? loczlizedPropertyName1 = GetLoczlizedPropertyName(localizer, propertyName);
        string? loczlizedPropertyName2 = GetLoczlizedPropertyName(localizer, name);
        return DefaultValidatorExtensions.GreaterThanOrEqualTo(ruleBuilder, expression).WithMessage(loczlizedPropertyName1 + " " + string.Format(localizer.ValidationLocalizer[nameof(GreaterThanOrEqualTo)].Value, loczlizedPropertyName2));
    }

    public static IRuleBuilderOptions<TEntity, TProperty> LessThan<TEntity, TProperty, Resource>(
      this IRuleBuilder<TEntity, TProperty> ruleBuilder,
      Expression<Func<TEntity, TProperty>> expression,
      IBaseLocalizer<TEntity, Resource> localizer)
      where TEntity : BaseEntity
      where TProperty : IComparable<TProperty>, IComparable
      where Resource : class
    {
        string? propertyName = ruleBuilder.GetPropertyName();
        string? name = (((MemberExpression) expression.Body).Member as PropertyInfo)?.Name;
        string? loczlizedPropertyName1 = GetLoczlizedPropertyName(localizer, propertyName);
        string? loczlizedPropertyName2 = GetLoczlizedPropertyName(localizer, name);
        return DefaultValidatorOptions.WithMessage(DefaultValidatorExtensions.LessThan(ruleBuilder, expression), loczlizedPropertyName1 + " " + string.Format(localizer.ValidationLocalizer[nameof(LessThan)].Value, loczlizedPropertyName2));
    }

    public static IRuleBuilderOptions<TEntity, TProperty> LessThanOrEqualTo<TEntity, TProperty, Resource>(
      this IRuleBuilder<TEntity, TProperty> ruleBuilder,
      Expression<Func<TEntity, TProperty>> expression,
      IBaseLocalizer<TEntity, Resource> localizer)
      where TEntity : BaseEntity
      where TProperty : IComparable<TProperty>, IComparable
      where Resource : class
    {
        string? propertyName = ruleBuilder.GetPropertyName<TEntity, TProperty>();
        string? name = ((expression.Body as MemberExpression).Member as PropertyInfo).Name;
        string? loczlizedPropertyName1 = GetLoczlizedPropertyName(localizer, propertyName);
        string? loczlizedPropertyName2 = GetLoczlizedPropertyName(localizer, name);
        return DefaultValidatorExtensions.LessThanOrEqualTo(ruleBuilder, expression).WithMessage(loczlizedPropertyName1 + " " + string.Format(localizer.ValidationLocalizer[nameof(LessThanOrEqualTo)].Value, loczlizedPropertyName2));
    }

    private static string? GetLoczlizedPropertyName<TEntity, Resource>(
      IBaseLocalizer<TEntity, Resource> localizer,
      string? propertyName)
      where TEntity : BaseEntity
      where Resource : class
    {
        propertyName = !localizer.SharedLocalizer.GetAllStrings(true).Any(v => v.Name.Equals(propertyName)) ? (string)localizer.EntityLocalizer[propertyName] : localizer.SharedLocalizer[propertyName];
        return propertyName;
    }

    public static IRuleBuilderOptions<TEntity, string> Length<TEntity, Resource>(
      this IRuleBuilder<TEntity, string> ruleBuilder,
      int min,
      int max,
      IBaseLocalizer<TEntity, Resource> localizer)
      where TEntity : BaseEntity
      where Resource : class
    {
        string? propertyName = ruleBuilder.GetPropertyName();
        string? loczlizedPropertyName = GetLoczlizedPropertyName(localizer, propertyName);
        return DefaultValidatorOptions.WithMessage<TEntity, string>(DefaultValidatorExtensions.Length(ruleBuilder, min, max), loczlizedPropertyName + " " + string.Format(localizer.ValidationLocalizer[nameof(Length)].Value, min, max));
    }

    public static IRuleBuilderOptions<TEntity, TProperty> NotNull<TEntity, TProperty, Resource>(
      this IRuleBuilder<TEntity, TProperty> ruleBuilder,
      IBaseLocalizer<TEntity, Resource> localizer)
      where TEntity : BaseEntity
      where Resource : class
    {
        string? propertyName = ruleBuilder.GetPropertyName();
        string? loczlizedPropertyName = GetLoczlizedPropertyName(localizer, propertyName);
        IRuleBuilderOptions<TEntity, TProperty> iruleBuilderOptions = DefaultValidatorExtensions.NotNull(ruleBuilder);
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
        interpolatedStringHandler.AppendFormatted(loczlizedPropertyName);
        interpolatedStringHandler.AppendLiteral(" ");
        interpolatedStringHandler.AppendFormatted(localizer.ValidationLocalizer["IsRequired"]);
        string stringAndClear = interpolatedStringHandler.ToStringAndClear();
        return DefaultValidatorOptions.WithMessage(iruleBuilderOptions, stringAndClear);
    }

    public static string? GetPropertyName<TEntity, TProperty>(
      this IRuleBuilder<TEntity, TProperty> ruleBuilder)
      where TEntity : BaseEntity
    {
        string? propertyName = null;
        DefaultValidatorOptions.WithMessage(DefaultValidatorExtensions.NotEmpty(ruleBuilder)
            ?.Configure(config => propertyName = config.PropertyName), " ");
        return propertyName;
    }
}