using Microsoft.Extensions.Options;

namespace SampleApp.Api;

public class DatabaseOptionsSetup : IConfigureOptions<DatabaseOptions>
{
    private readonly string _configurationSectionName = "DatabaseOptions";
    private readonly string _connectionStrigName = "BaseConnection";
    private readonly IConfiguration _configuration;

    public DatabaseOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(DatabaseOptions options)
    {
        var connectionString = _configuration.GetConnectionString(_connectionStrigName);

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new NotImplementedException("DatabaseOptionsSetup > Configure");

        options.ConnectionString = connectionString;

        _configuration.GetSection(_configurationSectionName).Bind(options);
    }
}
