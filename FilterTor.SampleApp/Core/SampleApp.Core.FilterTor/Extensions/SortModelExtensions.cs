namespace SampleApp.FilterTorEx;

using FilterTor.Models;
using System;

public static class SortModelExtensions
{
    public static EntityType GetEntityType(this SortModel sortModel)
    {
        return Enum.Parse<EntityType>(sortModel.Entity, ignoreCase: true);
    }

    public static T GetProperty<T>(this SortModel sortModel) where T : struct
    {
        return Enum.Parse<T>(sortModel.Property, ignoreCase: true);
    }
}
