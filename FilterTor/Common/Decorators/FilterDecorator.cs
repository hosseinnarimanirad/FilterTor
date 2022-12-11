﻿namespace GridEngineCore.Decorators;

using GridEngineCore.Common.Entities;
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

    private readonly IEntityResolver<T> _entityResolver;

    private readonly IQueryGenerator<T> _queryGenerator;

    public FilterDecorator(IQueryGenerator<T> queryGenerator, JsonConditionBase condition, IEntityResolver<T> entityResolver)
    {
        this._condition = condition;

        this._entityResolver = entityResolver;

        this._queryGenerator = queryGenerator;
    }

    public IQueryable<T> Query(IQueryable<T> list)
    {
        IQueryable<T> iqueryable = _queryGenerator.Query(list);

        var predicate = _entityResolver.GetPredicate(_condition);

        if (predicate is not null)
        {
            iqueryable = iqueryable.Where(predicate);
        }

        return iqueryable;
    }
}
