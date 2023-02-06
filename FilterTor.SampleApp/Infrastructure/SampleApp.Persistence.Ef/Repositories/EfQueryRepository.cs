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
     
    protected IQueryable<TEntity> Entities => _context.Set<TEntity>().AsNoTracking();

    protected EfQueryRepository(SampleAppContext dbContext)
    {
        this._context = dbContext; 
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
   
}