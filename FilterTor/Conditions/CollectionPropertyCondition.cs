namespace FilterTor;

using FilterTor.Resolvers;
using FilterTor.Conditions;
using FilterTor.Models;
using System;
using System.Collections.Generic;
using System.Linq;


// COMPOSITE DESIGN PATTERN:
// LEAF
public class CollectionPropertyCondition<TEntity> : ILeafCondition where TEntity : class
{
    public required string Entity { get; init; }

    public CategoryType Category => CategoryType.CollectionProperty;

    public FuncExp<TEntity, bool> Predicate { get; set; }

    public required Func<TEntity, object> ExtractValue { get; set; }

    public bool IsPassed(ConditionParameter<TEntity>? parameter)
    {
        if (parameter == null)
            return false;

        return this.Predicate.Func(parameter.Value) == true;
    }

    public IEnumerable<TEntity> Filter(IQueryable<TEntity> input)
    {
        return input/*.ToList()*/.Where(Predicate.Expression);
    }

    public bool IsPassed(IConditionParameter parameter)
    {
        return this.IsPassed(parameter as ConditionParameter<TEntity>);
    }


    public static CollectionPropertyCondition<T> Create<T>(JsonConditionBase jsonCondition, IEntityResolver<T> resolver) where T : class
    {
        var condition = jsonCondition as JsonCollectionPropertyCondition;

        if (condition == null)
            throw new NotImplementedException("CollectionPropertyCondition -> Create");

        return new CollectionPropertyCondition<T>()
        {
            Entity = condition.Entity,
            Predicate = new FuncExp<T, bool>(resolver.GetPropertyFilter(condition.Target, condition.Collection, condition.Operation.Value)),
            ExtractValue = resolver.ExtractPropertyValue(condition.Collection)
        };
    } 
}
