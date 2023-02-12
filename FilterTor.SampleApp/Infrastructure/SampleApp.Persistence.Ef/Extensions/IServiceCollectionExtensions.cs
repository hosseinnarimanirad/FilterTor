using Grid.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleApp.Application.Gateways;
using SampleApp.Persistence.Ef.Repositories;

namespace SampleApp.Persistence.Ef.Extensions;

public static class IServiceCollectionExtensions
{

    public static IServiceCollection ConfigureEfContext(this IServiceCollection service)
    {
        service.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

        service.AddDbContext<SampleAppContext>((serviceProvider, dbContextOptionsBuilder) =>
        {
            var databaseOptions = serviceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;

            dbContextOptionsBuilder.UseSqlServer(databaseOptions.ConnectionString, sqlServerAction =>
            {
                sqlServerAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                sqlServerAction.CommandTimeout(databaseOptions.CommandTimeout);
            });

            dbContextOptionsBuilder.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
            dbContextOptionsBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);

#if DEBUG
            LoggerFactory _loggerFactory = new LoggerFactory(new[] { new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider() });
            dbContextOptionsBuilder.UseLoggerFactory(_loggerFactory);
#endif

        });

        return service;
    }
}
