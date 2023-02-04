namespace SobaFw.Server;

public class IdentityOptionsInstaller : IInstaller
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        //services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
        //    .AddEntityFrameworkStores<ApplicationContext>()
        //    .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            // Password settings
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = false;

            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            options.Lockout.MaxFailedAccessAttempts = 10;
            options.Lockout.AllowedForNewUsers = true;

            // User settings
            options.User.RequireUniqueEmail = false;
        });

        //services.ConfigureApplicationCookie(options =>
        //{
        //    options.Cookie.HttpOnly = true;
        //    options.Events.OnRedirectToLogin = context =>
        //    {
        //        context.Response.StatusCode = 401;
        //        return Task.CompletedTask;
        //    };
        //});
    }
}