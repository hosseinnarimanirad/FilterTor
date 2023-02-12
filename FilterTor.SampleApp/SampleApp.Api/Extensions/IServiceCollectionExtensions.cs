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
}
