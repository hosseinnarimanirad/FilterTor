namespace Grid.Application;

using global::Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SampleApp.Application.Common.Mapster;

public static class StartupExtensions
{
    public static void ConfigureMapster(this IServiceCollection service)
    {
        var config = new TypeAdapterConfig();

        config.Apply(new ConditionProfile());

        service.AddSingleton(config); 
    }

    public static void ConfigureFluentValidation(this IServiceCollection service)
    {
        //service.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));
    }
}
