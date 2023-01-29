namespace FilterTor.Resolvers;

using System;
using FilterTor.Models;
using System.Linq.Expressions;
using FilterTor.Conditions;

public interface IConditionResolver<T>
{
    Expression<Func<T, bool>> ExtractPredicate(JsonConditionBase condition);
}