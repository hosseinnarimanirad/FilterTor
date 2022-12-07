namespace GridEngineCore.Decorators;

using System;
using GridEngineCore.Models;
using System.Linq.Expressions;
using GridEngineCore.Conditions;

public interface IConditionResolver<T>
{
    Expression<Func<T, bool>> ExtractPredicate(JsonConditionBase condition);
}