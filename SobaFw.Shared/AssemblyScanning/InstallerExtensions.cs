namespace SobaFw.Shared;

public static class InstallerExtensions
{
    public static void AddInstallerFromAssembly(this IServiceCollection services, IConfiguration configuration, Type type)
        => AddInstallerFromAssemblies(services, configuration, type.Assembly);
    public static void AddInstallerFromAssembly<TMarker>(this IServiceCollection services, IConfiguration configuration)
        => AddInstallerFromAssemblies(services, configuration, typeof(TMarker).Assembly);
    public static void AddInstallerFromAssembly(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
        => AddInstallerFromAssemblies(services, configuration, assembly);
    public static void AddInstallerFromReferancedAssemblies(this IServiceCollection services, IConfiguration configuration, Assembly assembly, string searchPattern)
    {
        Assembly[] referancedAssemblies = AssemblyExtensionsUtility.GetReferencedAssemblies(assembly, searchPattern);
        AddInstallerFromAssemblies(services, configuration, referancedAssemblies);
    }
    public static void AddInstallerFromAssemblies(this IServiceCollection services, IConfiguration configuration, params Assembly[] assmblies)
    {
        foreach (var assembly in assmblies)
        {
            IEnumerable<TypeInfo> installerTypes = assembly.DefinedTypes
                .Where(type => typeof(IInstaller).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);

            IEnumerable<IInstaller> installers = installerTypes.Select(Activator.CreateInstance)?.Cast<IInstaller>();
            foreach (var installer in installers)
            {
                installer.ConfigureServices(services, configuration);
            }
        }
    }
}