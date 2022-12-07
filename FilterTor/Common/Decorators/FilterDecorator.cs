namespace GridEngineCore.Decorators;

using GridEngineCore.Conditions;
using GridEngineCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

public class FilterDecorator<T> : IQueryGenerator<T>
{
    private readonly JsonConditionBase _condition;

    private readonly IFilterResolver<T> _filterResolver;

    private readonly IQueryGenerator<T> _queryGenerator;

    public FilterDecorator(IQueryGenerator<T> queryGenerator, JsonConditionBase condition, IFilterResolver<T> filterResolver)
    {
        this._condition = condition;

        this._filterResolver = filterResolver;

        this._queryGenerator = queryGenerator;
    }

    public IQueryable<T> Query(IQueryable<T> list)
    {
        IQueryable<T> iqueryable = _queryGenerator.Query(list);

        var predicate = _filterResolver.GetPredicate(_condition);

        if (predicate is not null)
        {
            iqueryable = iqueryable.Where(predicate);
        }

        return iqueryable;
    }
}
