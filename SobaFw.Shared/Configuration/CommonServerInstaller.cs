namespace SobaFw.Shared;

public class CommonServerInstaller : IInstaller
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IBaseLocalizer<,>), typeof(BaseLocalizer<,>));
    }
}