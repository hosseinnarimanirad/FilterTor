namespace GridEngineCore.Common.Entities;

using GridEngine.Factory;
using GridEngine;
using GridEngineCore.Conditions;
using GridEngineCore.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

public abstract class EntityResolver<T> : IEntityResolver<T> where T : class
{
    public virtual Expression<Func<T, bool>>? GetPredicate(JsonConditionBase jsonCondition)
    {
        ICondition? condition = ConditionFactory.Create<T>(jsonCondition, this);

        if (condition is null)
            return t => false;

        return ConditionFactory.GetExpression<T>(condition);
    }

    public abstract Func<T, object> ExtractPropertyValue(string propType); 

    public abstract Expression<Func<T, bool>> GetPropertyFilter(JsonTargetBase? target, string propType, Operation operation);
}
