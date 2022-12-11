namespace Grid.Application;

using global::Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SampleApp.Application;
using SampleApp.Application.Common.Mapster;
using Scrutor;

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

    public static void ScanInjections(this IServiceCollection services)
    {
        services.Scan(delegate (ITypeSourceSelector scan)
        {
            scan.FromApplicationDependencies().AddClasses(delegate (IImplementationTypeFilter classes)
            {
                classes.AssignableTo<ISingleton>();
            }).AsImplementedInterfaces()
                .WithSingletonLifetime()
                .AddClasses(delegate (IImplementationTypeFilter classes)
                {
                    classes.AssignableTo<IScoped>();
                })
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(delegate (IImplementationTypeFilter classes)
                {
                    classes.AssignableTo<ITransient>();
                })
                .AsImplementedInterfaces()
                .WithTransientLifetime();
        });
    }
}
