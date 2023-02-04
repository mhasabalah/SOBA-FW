namespace SobaFw.Client;

public static class ClientInstallerExtensions
{
    public static void AddInstallerFromAssembly(this WebAssemblyHostBuilder builder, Type type)
        => AddInstallerFromAssemblies(builder, type.Assembly);
    public static void AddInstallerFromAssembly<TMarker>(this WebAssemblyHostBuilder builder)
        => AddInstallerFromAssemblies(builder, typeof(TMarker).Assembly);
    public static void AddInstallerFromAssembly(this WebAssemblyHostBuilder builder, Assembly assembly)
        => AddInstallerFromAssemblies(builder, assembly);
    public static void AddInstallerFromReferancedAssemblies(this WebAssemblyHostBuilder builder, Assembly assembly, string searchPattern)
    {
        Assembly[] referancedAssemblies = AssemblyExtensionsUtility.GetReferencedAssemblies(assembly, searchPattern);
        AddInstallerFromAssemblies(builder, referancedAssemblies);
    }
    public static void AddInstallerFromAssemblies(this WebAssemblyHostBuilder builder, params Assembly[] assmblies)
    {
        foreach (var assembly in assmblies)
        {
            IEnumerable<TypeInfo> installerTypes = assembly.DefinedTypes
                .Where(type => typeof(IClientInstaller).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);

            IEnumerable<IClientInstaller> installers = installerTypes.Select(Activator.CreateInstance)?.Cast<IClientInstaller>();
            foreach (var installer in installers)
            {
                installer.Configure(builder.Services);
            }
        }
    }
}