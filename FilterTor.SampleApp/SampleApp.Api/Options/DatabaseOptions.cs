namespace SampleApp.Api;

public class DatabaseOptions
{
    public string ConnectionString { get; set; }= string.Empty;

    public int MaxRetryCount { get; set; } = 3;

    public int CommandTimeout { get; set; } = 30;

    public bool EnableSensitiveDataLogging { get; set; }

    public bool EnableDetailedErrors { get; set; }
}
