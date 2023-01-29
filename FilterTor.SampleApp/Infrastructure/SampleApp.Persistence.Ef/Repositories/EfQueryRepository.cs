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

public abstract class EfQueryRepository<TKey, TEntity> : IQueryRepository<TKey, TEntity>
    where TEntity : class, IHasKey<TKey>
    where TKey : struct
{
    protected readonly SampleAppContext _context;

    protected readonly ISortResolver<TEntity> _sortResolver;

    protected readonly IEntityResolver<TEntity> _entityResolver;

    protected IQueryable<TEntity> Entities => _context.Set<TEntity>().AsNoTracking();

    protected EfQueryRepository(SampleAppContext dbContext,
                                    ISortResolver<TEntity> sortResolver,
                                    IEntityResolver<TEntity> entityResolver)
    {
        this._context = dbContext;
        this._sortResolver = sortResolver;
        this._entityResolver = entityResolver;
    }


    public virtual ValueTask<TEntity> GetAsync(TKey id)
    {
        return _context.Set<TEntity>().FindAsync(id)!;
    }

    public virtual Task<List<TEntity>> GetAllByIdsAsync(IEnumerable<TKey> ids)
    {
        return _context.Set<TEntity>().Where(p => ids.Contains(p.Id)).ToListAsync();
    }

    public virtual Task<List<TEntity>> GetAllAsync()
    {
        return _context.Set<TEntity>().ToListAsync();
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