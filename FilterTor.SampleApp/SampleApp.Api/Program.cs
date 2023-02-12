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
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using SampleApp.FilterTorEx;
using SampleApp.FilterTorEx.Entities;
using SampleApp.Core.Entities;
using FilterTor.Resolvers;
using FilterTor.Strategies;
using SampleApp.Persistence.Ef;
using SampleApp.Presentation.Extensions;
using SampleApp.Persistence.Ef.Extensions;
using SampleApp.FilterTorEx.Extensions;
using SampleApp.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseCustomSerilog();

builder.Services.ConfigureServiceLifetimes()
    .ConfigureControllers()
    .ConfigureMediatR()
    .ConfigureOptions<DatabaseOptionsSetup>()
    .ConfigureEfContext()
    .ConfigureMapster()
    .ConfigureFluentValidation()
    .ConfigureFilterTorServices()
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    .AddEndpointsApiExplorer()
    .ConfigureSwagger();

 

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
            // 1401.10.19
            // cannot be used with MigrateAsync
            //await appDbContext.Database.EnsureCreatedAsync();
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
