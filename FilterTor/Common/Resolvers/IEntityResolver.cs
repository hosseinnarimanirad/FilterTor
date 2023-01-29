namespace FilterTor.Resolvers;

using FilterTor.Conditions;
using FilterTor.Targets;
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

    bool Validate(JsonConditionBase jsonCondition);

    //IQueryable<T> GetSource();
}
