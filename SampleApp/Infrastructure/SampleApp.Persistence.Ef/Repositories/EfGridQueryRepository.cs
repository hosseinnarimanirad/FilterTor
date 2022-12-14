namespace SampleApp.Persistence.Ef.Repositories;

using FilterTor.Common.Entities;
using FilterTor.Conditions;
using FilterTor.Decorators;
using FilterTor.Extensions;
using FilterTor.Models;
using Grid.Persistence;
using Microsoft.EntityFrameworkCore;
using SampleApp.Application.Gateways;
using SampleApp.Application.Gateways.Repositories;
using SampleApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class EfGridQueryRepository<TKey, TEntity> : EfQueryRepository<TKey, TEntity>,
                                                                IGridQueryRepository<TKey, TEntity>
                                                                where TEntity : class, IHasKey<TKey>
                                                                where TKey : struct
{
    protected readonly ISortResolver<TEntity>? _sortResolver;
    protected readonly IEntityResolver<TEntity>? _entityResolver;

    //protected EfGridQueryRepository(SampleAppContext dbContext) : base(dbContext)
    //{

    //}

    protected EfGridQueryRepository(SampleAppContext dbContext,
                                    ISortResolver<TEntity> sortResolver,
                                    IEntityResolver<TEntity> entityResolver) : base(dbContext)
    {
        this._sortResolver = sortResolver;
        this._entityResolver = entityResolver;
    }

    public IQueryable<TEntity> Query(IQueryable<TEntity> list)
    {
        // simple decorator that does noting just to help
        // building the chain of decorator classes
        return list;
    }


    public async Task<List<TEntity>> Filter(JsonConditionBase? jsonCondition, List<SortModel>? sorts, PagingModel? paging)
    {
        IQueryGenerator<TEntity> result = this;

        if (jsonCondition is not null)
        {
            result = new FilterDecorator<TEntity>(result, jsonCondition, _entityResolver);
        }

        if (!sorts.IsNullOrEmpty())
        {
            result = new SortDecorator<TEntity>(result, sorts, _sortResolver);
        }

        if (paging is not null)
        {
            result = new PagingDecorator<TEntity>(result, paging);
        }

        return await result.Query(this._context.Set<TEntity>().AsQueryable()).ToListAsync();
    }
}
