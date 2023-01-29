namespace FilterTor.Resolvers;

using FilterTor.Conditions;
using FilterTor.Factory;
using FilterTor.Helpers;
using FilterTor.Targets;
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
        Validate(jsonCondition);

        ICondition? condition = ConditionFactory.Create(jsonCondition, this);

        if (condition is null)
            return t => false;

        return ConditionFactory.GetExpression<T>(condition);
    }

    public abstract Func<T, object> ExtractPropertyValue(string propType);

    public abstract Expression<Func<T, bool>> GetPropertyFilter(JsonTargetBase? target, string propType, Operation operation);

    public abstract bool Validate(JsonConditionBase jsonCondition);

    public abstract bool Validate(JsonTargetBase? jsonTarget);

    protected bool Validate<TProperty, TCollectionProperty, TMeasure>(JsonConditionBase jsonCondition)
        where TProperty : struct
        where TCollectionProperty : struct
        where TMeasure : struct
    {
        switch (jsonCondition)
        {
            case JsonCompoundCondition condition:
                return condition.Validate() && condition.Conditions.All(Validate);

            case JsonPropertyCondition condition:
                return condition.Validate() &&
                        EnumHelper.IsDefined<TProperty>(condition.Property) &&
                        Validate(condition.Target);

            case JsonCollectionPropertyCondition condition:
                return condition.Validate() &&
                        EnumHelper.IsDefined<TCollectionProperty>(condition.Collection) &&
                        Validate(condition.Target);

            case JsonMeasureCondition condition:
                return condition.Validate() &&
                        EnumHelper.IsDefined<TMeasure>(condition.Measure) &&
                        Validate(condition.Target);

            default:
                return false;
        }
    }

    protected bool Validate<TProperty, TCollectionProperty, TMeasure>(JsonTargetBase? jsonTarget)
        where TProperty : struct
        where TCollectionProperty : struct
        where TMeasure : struct
    {
        switch (jsonTarget)
        {
            case JsonPropertyTarget target:
                return EnumHelper.IsDefined<TProperty>(target.Property);

            case JsonCollectionPropertyTarget target:
                return EnumHelper.IsDefined<TCollectionProperty>(target.Collection);

            case JsonMeasureTarget target:
                return EnumHelper.IsDefined<TMeasure>(target.Measure);

            case JsonConstantTarget:
            case JsonArrayTarget:
            case JsonRangeTarget:
                return true;

            default:
                return false;
        }
    }

    //public abstract IQueryable<T> GetSource();
}
