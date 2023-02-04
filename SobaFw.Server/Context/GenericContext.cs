namespace SobaFw.Server;

public class GenericContext: IdentityDbContext<IdentityUser>
{
    public string? ApplicationDbConnectionString { get; set; }
    public List<Assembly>? RefrencedAssemblies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        try
        {
            if (optionsBuilder.IsConfigured || ApplicationDbConnectionString == null)
                return;
            optionsBuilder.UseSqlServer(ApplicationDbConnectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution).EnableSensitiveDataLogging().EnableDetailedErrors();
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        //GenericContext.a a = new GenericContext.a();
        //a.a = modelBuilder;
        //try
        //{
        //    base.OnModelCreating(a.a);
        //    List<Assembly>? refrencedAssemblies = RefrencedAssemblies;
        //    if ((refrencedAssemblies != null ? (refrencedAssemblies.Any() ? 1 : 0) : 0) == 0)
        //        return;
        //    RefrencedAssemblies?.ForEach(new Action<Assembly>(a.a));
        //}
        //catch (Exception ex)
        //{
        //    Log.Error(ex.Message);
        //}
    }

    public IDbContextTransaction? GetTransaction() => this.Database?.CurrentTransaction != null ? (IDbContextTransaction)null! : this.Database?.BeginTransaction();
}