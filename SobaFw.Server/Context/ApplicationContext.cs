namespace SobaFw.Server;

public class ApplicationContext: IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    protected ApplicationContext(DbContextOptions<ApplicationContext> contextOptions) : base(contextOptions) { }

    public ApplicationContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            SqlServerDbContextOptionsExtensions.UseSqlServer(optionsBuilder, "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FFMS_API;Integrated Security=True;", (Action<SqlServerDbContextOptionsBuilder>)null);
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        ApplyReferencedMigrationConfigurations(builder);
    }

    private void ApplyReferencedMigrationConfigurations(ModelBuilder modelBuilder)
    {
        foreach (Assembly assemblyReferencedAssembly in AssemblyExtensionsUtility.GetReferencedAssemblies(Assembly.GetExecutingAssembly(), "*.Server.dll"))
        {
            try
            {
                modelBuilder.ApplyConfigurationsFromAssembly(assemblyReferencedAssembly);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //Log.Error(ex);
            }
        }
    }

}