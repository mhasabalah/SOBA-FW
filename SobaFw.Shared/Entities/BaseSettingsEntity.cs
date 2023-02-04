namespace SobaFw.Shared;

public abstract class BaseSettingsEntity : BaseEntity
{
    public string? Name { get; set; }
    public string? NameSecondLanguage { get; set; }
}
