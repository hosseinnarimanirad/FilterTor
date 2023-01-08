namespace SampleApp.Api;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.SystemConsole.Themes;


public static class IHostBuilderExtensions
{
    public static IHostBuilder UseCustomSerilog(this IHostBuilder builder)
    {
        return builder.UseSerilog((context, configuration) =>
        {
            var serilogOptions = context.Configuration.GetSection("Serilog").Get<SerilogOptions>();

            if (serilogOptions.HasConsole())
            {
                configuration.WriteTo.Console(theme: SystemConsoleTheme.Literate, restrictedToMinimumLevel: serilogOptions.MinimumLevel.Default);
            }

            if (serilogOptions.HasElasticsearch())
            {
                var elasticConfig = serilogOptions.GetElasticsearchOptions(context.HostingEnvironment);
                if (elasticConfig is not null)
                {
                    configuration.WriteTo.Elasticsearch(
                        new ElasticsearchSinkOptions(elasticConfig.NodeUris.ToUri())
                        {
                            //ModifyConnectionSettings = x => x.Insecure(elasticConfig.ConnectionGlobalHeaders, elasticConfig.Insecure),
                            MinimumLogEventLevel = serilogOptions.MinimumLevel.Default,
                            AutoRegisterTemplate = elasticConfig.AutoRegisterTemplate,
                            OverwriteTemplate = elasticConfig.OverwriteTemplate,
                            DetectElasticsearchVersion = false,
                            AutoRegisterTemplateVersion = elasticConfig.AutoRegisterTemplateVersion,
                            NumberOfReplicas = elasticConfig.NumberOfReplicas,
                            NumberOfShards = elasticConfig.NumberOfShards,
                            TypeName = elasticConfig.TypeName,
                            IndexFormat = elasticConfig.IndexFormat,
                            BatchAction = elasticConfig.BatchAction,
                            BufferBaseFilename = elasticConfig.BufferBaseFilename,
                            RegisterTemplateFailure = RegisterTemplateRecovery.IndexAnyway,
                            FailureCallback = e => Console.WriteLine("Unable to submit event " + e.MessageTemplate),
                            EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog | EmitEventFailureHandling.WriteToFailureSink | EmitEventFailureHandling.RaiseCallback,
                            //LevelSwitch = ElasticsearchLevelSwitch.Apply(elasticConfig, serilogOptions.MinimumLevel),
                        });
                }
            }
        });
    }

    public static bool HasConsole(this SerilogOptions serilog)
    {
        return serilog.WriteTo?.Any(q => q.Name.ToLower() == "console") ?? false;
    }

    public static bool HasElasticsearch(this SerilogOptions serilog)
    {
        return serilog.WriteTo?.Any(q => q.Name.ToLower() == "elasticsearch") ?? false;
    }

    public static ArgsOptions GetElasticsearchOptions(this SerilogOptions serilog, IHostEnvironment environment)
    {
        var elasticsearch = serilog.WriteTo.FirstOrDefault(q => q.Name.ToLower() == "elasticsearch");
        if (elasticsearch is null || elasticsearch.Args is null) return null;

        if (string.IsNullOrEmpty(elasticsearch.Args.IndexFormat))
        {
            elasticsearch.Args.IndexFormat =
                $"{Assembly.GetEntryAssembly()?.GetName().Name}-logs-" +
                $"{environment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}";
        }

        return elasticsearch.Args;
    }

    public static List<Uri> ToUri(this string nodes)
    {
        return nodes
            .Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(uriString => new Uri(uriString)).ToList();
    }
}