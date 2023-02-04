namespace SobaFw.Client;
public class CommonClientInstaller : IClientInstaller
{
    public void Configure(IServiceCollection services)
    {
        services.AddOptions();
        services.AddAuthorizationCore();
        services.AddScoped<IdentityAuthenticationStateProvider>();
        services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<IdentityAuthenticationStateProvider>());
        services.AddScoped<IAuthorizeApi, AuthorizeApi>();
        
        services.AddLocalization();
        services.AddScoped(typeof(IBaseLocalizer<,>), typeof(BaseLocalizer<,>));
        services.AddSingleton<AppObserver>();
    }
}