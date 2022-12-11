using Grid.Application;
using Grid.Persistence;
using MediatR;
using Microsoft.Extensions.Options;
using SampleApp.Api;
using SampleApp.Application.Gateways;
using SampleApp.Persistence.Ef.Repositories;
using SampleApp.Presentation.Controllers;
using System.Reflection;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ScanInjections();

builder.Services.AddControllers()
                .AddApplicationPart(typeof(TableController).Assembly)
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    //options.JsonSerializerOptions.Converters.Add(new ConditionJsonConverter());
                    //options.JsonSerializerOptions.Converters.Add(new TargetJsonConverter());
                });


builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(Assembly.GetAssembly(typeof(StartupExtensions))!);


builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));


builder.Services.ConfigureOptions<DatabaseOptionsSetup>();

builder.Services.AddDbContext<SampleAppContext>((serviceProvider, dbContextOptionsBuilder) =>
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


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Services.ConfigKafka();

builder.Services.ConfigureMapster();
builder.Services.ConfigureFluentValidation();



// **********************************************************
// **********************************************************
var app = builder.Build();

bool debug = false;

#if DEBUG
debug = true;
#endif

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

var logger = app.Services.GetService<Serilog.ILogger>();

try
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        var appDbContext = services.GetService<SampleAppContext>();
        
        logger.Information("Migrating Database...");

        if (appDbContext is null)
        {
            logger.Error("Can not get Database!");
        }
        else
        {
            await appDbContext.Database.MigrateAsync();
            logger.Information("Migrated Successfully");
        }
    }

    logger?.Information("Starting web host, env: {env}", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));

    app.Run();
}
catch (Exception ex)
{
    logger?.Fatal(ex, "Host terminated unexpectedly. Message: {ex.message}, InnerException: {ex.InnerException.Message}", ex.Message, ex.InnerException?.Message);
}
