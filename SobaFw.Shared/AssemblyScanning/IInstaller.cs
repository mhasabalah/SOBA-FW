namespace SobaFw.Shared;

public interface IInstaller
{
    void ConfigureServices(IServiceCollection services, IConfiguration configuration);
}