namespace SampleApp.Application.Extensions;

using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class MapsterExtensions
{
    public static TDestination? Adapt<TDestination>(this object source, Action<TDestination> customUpdateAction)
    {
        if (source is null)
            return default;

        var result = source.Adapt<TDestination>();

        if (result is not null)
        {
            customUpdateAction(result);
        }

        return result;
    }
}
