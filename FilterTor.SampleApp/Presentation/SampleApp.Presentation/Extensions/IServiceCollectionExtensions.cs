using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SampleApp.Presentation.Extensions;

public static class IServiceCollectionExtensions
{

    // APPROACH #1
    //public static IServiceCollection ConfigureControllers(this IServiceCollection services)
    //{
    //    services.AddControllers()
    //                    .AddApplicationPart(typeof(TableController).Assembly)
    //                    .AddJsonOptions(options =>
    //                    {
    //                        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    //                        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    //                    });

    //    return services;
    //}

    // APPROACH #2
    public static IServiceCollection ConfigureControllers(this IServiceCollection services/*, IConfiguration config*/)
    {
        services.AddControllers(
            config => config.RespectBrowserAcceptHeader = true
            )
            // to enable "Content Negotiation"
            //.AddXmlDataContractSerializerFormatters()
            .ConfigureApplicationPartManager(manager =>
            {
                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            }); ;

        return services;
    }

}