namespace SobaFw.Shared;

public class AssemblyExtensionsUtility
{
    public static Assembly[] GetReferencedAssemblies(Assembly assembly, string searchPattern)
    {
        string[] files = Directory.GetFiles(new FileInfo(assembly.Location).DirectoryName, searchPattern);
        return files?.Select(e => Assembly.LoadFrom(e)).ToArray();
    }
}