namespace SampleApp.Persistence.Ef.Repositories;

using FilterTor.Conditions;
using FilterTor.Decorators;
using FilterTor.Extensions;
using FilterTor.Models;
using FilterTor.Resolvers;
using Grid.Persistence;
using Microsoft.EntityFrameworkCore;
using SampleApp.Application.Gateways;
using SampleApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class EfFilterTorRepository<TKey, TEntity> : EfQueryRepository<TKey, TEntity>, IQueryGenerator<TEntity>
                                                                where TEntity : class, IHasKey<TKey>
                                                                where TKey : struct
{
    protected EfFilterTorRepository(SampleAppContext dbContext,
                                    ISortResolver<TEntity> sortResolver,
                                    IEntityResolver<TEntity> entityResolver) : base(dbContext, sortResolver, entityResolver)
    {
    }


    public virtual IQueryable<TEntity> Query(IQueryable<TEntity> list)
    {
        // simple decorator that does noting just to help
        // building the chain of decorator classes
        return list;
    }

    public virtual async Task<List<TEntity>> Filter(JsonConditionBase? jsonCondition, List<SortModel>? sorts, PagingModel? paging)
    {
        IQueryGenerator<TEntity> result = this;

        if (jsonCondition is not null)
        {
            result = new FilterDecorator<TEntity>(result, jsonCondition, _entityResolver);
        }

        if (!sorts.IsNullOrEmpty())
        {
            result = new SortDecorator<TEntity>(result, sorts!, _sortResolver);
        }

        if (paging is not null)
        {
            result = new PagingDecorator<TEntity>(result, paging);
        }

        return await result.Query(this._context.Set<TEntity>().AsQueryable()).ToListAsync();
    }
}