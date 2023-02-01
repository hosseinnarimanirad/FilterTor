namespace SampleApp.FilterTorEx;

using FilterTor;
using FilterTor.Conditions;
using FilterTor.Helpers;
using System;


public static class FilterTorExtensions
{
    public static EntityType GetEntityType(this JsonLeafCondition condition)
    {
        //return Enum.Parse<EntityType>(condition.Entity, ignoreCase: true);
        return EnumHelper.Parse<EntityType>(condition.Entity);
    }

    public static EntityType GetEntityType(this ILeafCondition condition)
    {
        //return Enum.Parse<EntityType>(condition.Entity, ignoreCase: true);
        return EnumHelper.Parse<EntityType>(condition.Entity);
    }   
}