using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using System.Collections.Generic;

namespace SampleApp.Api;

public class SerilogOptions
{
    public MinimumLevelOptions MinimumLevel { get; set; } = new();

    public List<SinkOptions>? WriteTo { get; set; }
}

public class MinimumLevelOptions
{
    public LogEventLevel Default { get; set; } = LogEventLevel.Error;

    public OverrideOptions Override { get; set; } = new();
}

public class OverrideOptions
{
    public LogEventLevel Default { get; set; } = LogEventLevel.Error;

    public LogEventLevel? Microsoft { get; set; }
}

public class SinkOptions
{
    public string Name { get; set; }

    public ArgsOptions Args { get; set; }
}

public class ArgsOptions
{
    public string NodeUris { get; set; } = "http://localhost:9200;";

    public bool Insecure { get; set; } = true;

    public string? IndexFormat { get; set; }

    public string TypeName { get; set; } = null;

    public ElasticOpType BatchAction { get; set; } = ElasticOpType.Index;

    public LogEventLevel? RestrictedToMinimumLevel { get; set; }

    public string BufferBaseFilename { get; set; } = "./logs/elasticsearch";

    public string ConnectionGlobalHeaders { get; set; } = "Authorization=Basic ZWxhc3RpYzplbGsxMzYy";

    public bool OverwriteTemplate { get; set; } = false;

    public AutoRegisterTemplateVersion AutoRegisterTemplateVersion { get; set; } = AutoRegisterTemplateVersion.ESv7;

    public bool AutoRegisterTemplate { get; set; } = true;

    public int NumberOfShards { get; set; } = 1;

    public int NumberOfReplicas { get; set; } = 1;
}