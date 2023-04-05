namespace SampleApp.Persistence;

using FilterTor;
using SampleApp.Core.Entities;
using SampleApp.Persistence.Ef.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.Extensions.Logging;

public class SampleAppContext : DbContext
{
    // just used in migration
    const string connection = "Server=.\\SQLEXPRESS;Database=FilterTorSample;Trusted_Connection=True";

    public DbSet<PrizeStore> FilterStore { get; set; }

    public DbSet<Invoice> Invoices { get; set; }

    public DbSet<InvoiceDetail> InvoiceDetails { get; set; }


    public SampleAppContext()
    {

    }

    public SampleAppContext(DbContextOptions<SampleAppContext> options) 
    {
    }

    //    private static DbContextOptions GetOptions(string connectionString)
    //    {
    //        var dbContextOptionsBuilder = new DbContextOptionsBuilder();

    //        dbContextOptionsBuilder.EnableDetailedErrors(detailedErrorsEnabled: true);
    //        dbContextOptionsBuilder.EnableSensitiveDataLogging(sensitiveDataLoggingEnabled: true);

    //#if DEBUG

    //        LoggerFactory _loggerFactory = new LoggerFactory(new[] { new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider() });
    //        dbContextOptionsBuilder.UseLoggerFactory(_loggerFactory);
    //#endif

    //        return SqlServerDbContextOptionsExtensions.UseSqlServer(dbContextOptionsBuilder, connectionString).Options;
    //    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 1401.08.18
        modelBuilder.ConfigureInterfaces();

        // APPROACH #1 - 1401.08.08
        //modelBuilder.ApplyConfigurationsFromAssembly(
        //    Assembly.GetExecutingAssembly(),
        //    t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

        // APPROACH #2
        //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // APPROACH #3 - 1401.11.25
        // SUGGESTED BY Milan Jovanovic
        var assembly = typeof(SampleAppContext).Assembly;

        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.EnableDetailedErrors(detailedErrorsEnabled: true);
            optionsBuilder.EnableSensitiveDataLogging(sensitiveDataLoggingEnabled: true);

#if DEBUG
            LoggerFactory _loggerFactory = new LoggerFactory(new[] { new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider() });
            optionsBuilder.UseLoggerFactory(_loggerFactory);
#endif

            _ = optionsBuilder.UseSqlServer(connection, options => options.CommandTimeout((int)TimeSpan.FromMinutes(2).TotalSeconds));
        }
    }
}
