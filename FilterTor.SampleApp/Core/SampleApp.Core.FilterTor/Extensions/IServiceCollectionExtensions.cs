using FilterTor.Resolvers;
using FilterTor.Strategies;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.FilterTorEx.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection ConfigureFilterTorServices(this IServiceCollection service)
    {
        service.AddScoped(typeof(SingleSourceStrategy<>))
                .AddScoped(typeof(FilterTorStrategyContext<>))
                .ConfigureSortResolvres()
                .ConfigureEntityResolvres();


        return service;
    }

    private static IServiceCollection ConfigureEntityResolvres(this IServiceCollection service)
    {
        //typeof(EntityType).Assembly
        Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(item => item.GetInterfaces()
                                .Where(i => i.IsGenericType).Any(i => i.GetGenericTypeDefinition() == typeof(IEntityResolver<>)) && !item.IsAbstract && !item.IsInterface)
            .ToList()
            .ForEach(assignedTypes =>
            {
                var serviceType = assignedTypes.GetInterfaces().First(i => i.GetGenericTypeDefinition() == typeof(IEntityResolver<>));
                service.AddScoped(serviceType, assignedTypes);
            });

        return service;
    }

    private static IServiceCollection ConfigureSortResolvres(this IServiceCollection service)
    {
        //typeof(EntityType).Assembly
        Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(item => item.GetInterfaces()
                                .Where(i => i.IsGenericType).Any(i => i.GetGenericTypeDefinition() == typeof(ISortResolver<>)) && !item.IsAbstract && !item.IsInterface)
            .ToList()
            .ForEach(assignedTypes =>
            {
                var serviceType = assignedTypes.GetInterfaces().First(i => i.GetGenericTypeDefinition() == typeof(ISortResolver<>));
                service.AddScoped(serviceType, assignedTypes);
            });

        return service;
    }
}
