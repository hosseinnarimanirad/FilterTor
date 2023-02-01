namespace FilterTor.Resolvers;

using FilterTor.Conditions;
using FilterTor.Extensions;
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

    public abstract List<string> SecondaryConditions { get;  }

    /// <summary>
    /// traverse all conditins
    /// </summary>
    /// <param name="jsonCondition"></param>
    /// <returns></returns>
    public bool HasSecondaryCondition(JsonConditionBase jsonCondition)
    {
        var list = jsonCondition.GetSubConditions();

        return list.Any(SecondaryConditions.Contains);
    }

    /// <summary>
    /// returns false for compound condition
    /// </summary>
    /// <param name="jsonCondition"></param>
    /// <returns></returns>
    private bool IsSecondaryCondition(JsonConditionBase jsonCondition)
    {
        if (jsonCondition.Category == CategoryType.Compound)
            return false;

        return jsonCondition.GetSubConditions().Any(SecondaryConditions.Contains);
    }

    /// <summary>
    /// Purify condition from secondary conditions
    /// </summary>
    /// <param name="jsonCondition"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public JsonConditionBase? GetPrimaryCondition(JsonConditionBase jsonCondition)
    {
        if (SecondaryConditions.IsNullOrEmpty())
            return jsonCondition;

        if (!HasSecondaryCondition(jsonCondition))
            return jsonCondition;

        string valueSubConditionn = string.Empty;

        switch (jsonCondition)
        {
            case JsonCompoundCondition jcc:
                if (jcc.IsAndMode is false && jcc.Conditions.Any(IsSecondaryCondition))
                    return null;

                var filteredConditions = jcc.Conditions.Select(GetPrimaryCondition).Where(c => c is not null).ToList();

                if (filteredConditions.IsNullOrEmpty())
                    return null;

                else
                    return JsonCompoundCondition.Create(jcc.IsAndMode, filteredConditions!);

            case JsonPropertyCondition jpc:
                return SecondaryConditions.Contains(jpc.Property) ? null : jpc;

            case JsonCollectionPropertyCondition jcp:
                return SecondaryConditions.Contains(jcp.Collection) ? null : jcp;

            case JsonMeasureCondition jmc:
                return SecondaryConditions.Contains(jmc.Measure) ? null : jmc;

            case JsonListCondition jlc:
                return SecondaryConditions.Contains(jlc.Measure) ? null : jlc;

            default:
                throw new NotImplementedException();
        }
    }

}
