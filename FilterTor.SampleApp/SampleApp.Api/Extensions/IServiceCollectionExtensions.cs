namespace SampleApp.Api.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        return services
            .AddSwaggerGen(s =>
            {
                s.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"SampleApp.Presentation.Swagger.xml"));
            });
    }

    // If we want to send requests from a different domain
    // to our application, configuring CORS is mandatory.
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(
            options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                        // in the case to be more restrictive
                        // restrict access from specific source
                        //.WithOrigins("https://example.com")
                        builder.AllowAnyOrigin()

                        // to restrict methods
                        //.WithMethods("POST", "GET")
                        .AllowAnyMethod()

                        // to restrict headers:
                        //.WithHeaders("accept", "content-type")
                        .AllowAnyHeader());
            });

    public static void ConfigureIISIntegration(this IServiceCollection services) =>
        services.Configure<IISOptions>(options => { });
}
