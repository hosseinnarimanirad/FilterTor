namespace SampleApp.Core.FilterTor;

using global::FilterTor;
using global::FilterTor.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class JsonConditionExtensions
{
    public static EntityType GetEntityType(this JsonLeafCondition condition)
    {
        return Enum.Parse<EntityType>(condition.Entity, ignoreCase: true);
    }

    public static EntityType GetEntityType(this ILeafCondition condition)
    {
        return Enum.Parse<EntityType>(condition.Entity, ignoreCase: true);
    }   
}