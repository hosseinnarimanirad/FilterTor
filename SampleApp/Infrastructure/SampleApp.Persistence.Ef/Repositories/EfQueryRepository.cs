namespace SampleApp.Persistence.Ef.Repositories;

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

public abstract class EfQueryRepository<TKey, TEntity> : IEfQueryRepository<TKey, TEntity>
    where TEntity : class, IHasKey<TKey>
    where TKey : struct
{
    protected readonly GridContext _context;

    protected IQueryable<TEntity> Entities => _context.Set<TEntity>().AsNoTracking();

    protected EfQueryRepository(GridContext dbContext)
    {
        _context = dbContext;
    }

    public virtual async Task<TEntity> GetAsync(TKey key)
    {
        return (await Entities.FirstOrDefaultAsync(f => f.Id.Equals(key)))!;
    }

    public virtual async Task<List<TEntity>> GetAllAsync()
    {
        return await Entities.ToListAsync();
    }

    public virtual async Task<List<TEntity>> GetAllByIdsAsync(List<TKey> keys)
    {
        return await Entities.Where(i => keys.Contains(i.Id)).ToListAsync();
    }
}