namespace Grid.Application;

using global::Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SampleApp.Application;
using SampleApp.Application.Common.Mapster;
using Scrutor;
using System.Reflection;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection ConfigureMapster(this IServiceCollection service)
    {
        var config = new TypeAdapterConfig();

        config.Apply(new ConditionProfile());

        service.AddSingleton(config);

        return service;
    }

    public static IServiceCollection ConfigureFluentValidation(this IServiceCollection service)
    {
        //service.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));

        return service;
    }

    public static IServiceCollection ConfigureServiceLifetimes(this IServiceCollection services)
    {
        services.Scan(delegate (ITypeSourceSelector scan)
        {
            scan.FromApplicationDependencies()
                .AddClasses(delegate (IImplementationTypeFilter classes)
                {
                    classes.AssignableTo<ISingleton>();
                })
                .AsImplementedInterfaces()
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

        return services;
    }

    public static IServiceCollection ConfigureMediatR(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        //services.AddMediatR(Assembly.GetAssembly(typeof(IServiceCollectionExtensions))!);

        return services;
    }
}
