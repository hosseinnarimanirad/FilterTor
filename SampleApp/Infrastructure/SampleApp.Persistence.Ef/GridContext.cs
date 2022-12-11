namespace Grid.Persistence;

using FilterTor;
using SampleApp.Core.Entities;
using Grid.Persistence.Ef.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


public class GridContext : DbContext
{
    // just used in migration
    string connection = "Server=.\\SQLEXPRESS;Database=GridDb;Trusted_Connection=True;";
     
    public DbSet<PolyFilter> PolyFilters { get; set; }
       
    public DbSet<Invoice> Invoices { get; set; }

    public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
     

    public GridContext()
    {

    }

    public GridContext(string connectionString) : base(GetOptions(connectionString))
    {
        connection = connectionString;
    }

    public GridContext(DbContextOptions<GridContext> options) : base(options)
    {
    }

    private static DbContextOptions GetOptions(string connectionString)
    {
        return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 1401.08.18
        modelBuilder.ConfigureInterfaces();

        // 1401.08.08
        //modelBuilder.ApplyConfigurationsFromAssembly(
        //    Assembly.GetExecutingAssembly(),
        //    t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

        // 1401.08.08
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            _ = optionsBuilder.UseSqlServer(connection,
                o => o.CommandTimeout((int)TimeSpan.FromMinutes(2).TotalSeconds));
        }

    }

}
