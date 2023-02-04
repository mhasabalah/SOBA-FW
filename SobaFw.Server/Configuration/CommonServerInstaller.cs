namespace SobaFw.Server;
public class CommonServerInstaller : IInstaller
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped(typeof(IBaseSettingsRepository<>), typeof(BaseSettingsRepository<>));
        services.AddScoped(typeof(IBaseUnitOfWork<>), typeof(BaseUnitOfWork<>));
        services.AddScoped(typeof(IBaseSettingsUnitOfWork<>), typeof(BaseSettingsUnitOfWork<>));

        services.AddLocalization();
        services.AddScoped(typeof(IBaseLocalizer<,>), typeof(BaseLocalizer<,>));

        //services.AddFluentValidation(options =>     //habqa a3del lesa haga  
        //{
        //    options.AutomaticValidationEnabled = true;
        //    options.DisableDataAnnotationsValidation = true;
        //});

        //services.AddFluentValidationAutoValidation().addf;
    }
}