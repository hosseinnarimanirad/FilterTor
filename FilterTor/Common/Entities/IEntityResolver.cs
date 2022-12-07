namespace GridEngineCore.Common.Entities;

using GridEngineCore.Conditions;
using GridEngineCore.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

public interface IEntityResolver<T>
{
    Expression<Func<T, bool>>? GetPredicate(JsonConditionBase jsonCondition);

    Expression<Func<T, bool>> GetPropertyFilter(JsonTargetBase? target, string propType, Operation operation);

    Func<T, object> ExtractPropertyValue(string propType);
}
