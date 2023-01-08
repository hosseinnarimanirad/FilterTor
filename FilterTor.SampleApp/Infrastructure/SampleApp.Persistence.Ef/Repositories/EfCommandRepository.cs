namespace SampleApp.Persistence.Ef.Repositories;

using EFCore.BulkExtensions;
using Grid.Persistence;
using Microsoft.EntityFrameworkCore;
using SampleApp.Application.Gateways;
using SampleApp.Application.Gateways.Repositories;
using SampleApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

public abstract class EfCommandRepository<TKey, TEntity> : IEfCommandRepository<TKey, TEntity>
    where TEntity : class, IHasKey<TKey>
    where TKey : struct
{
    protected readonly SampleAppContext _context;

    private readonly DbSet<TEntity> _dbSet;

    protected EfCommandRepository(SampleAppContext dbContext)
    {
        _context = dbContext;

        _dbSet = dbContext.Set<TEntity>();
    }

    public virtual ValueTask<TEntity> GetAsync(TKey id)
    {
        return _dbSet.FindAsync(id)!;
    }

    public virtual Task<List<TEntity>> GetAllByIdsAsync(IEnumerable<TKey> ids)
    {
        return _dbSet.Where(p => ids.Contains(p.Id)).ToListAsync();
    }

    public virtual Task<List<TEntity>> GetAllAsync()
    {
        return _dbSet.ToListAsync();
    }

    public void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task<TEntity> GetOrCreateAsync(Expression<Func<TEntity, bool>> predicate, Func<TEntity> createFunc)
    {
        var oldEntity = await _dbSet.Where(predicate).SingleOrDefaultAsync();

        if (oldEntity == null)
        {
            return createFunc();
        }
        else
        {
            return oldEntity;
        }
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        _dbSet.AddRange(entities);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public void BulkInsert(List<TEntity> entities)
    {
        _context.BulkInsert(entities);
    }

    public async Task BulkInsertAsync(List<TEntity> entities)
    {
        await _context.BulkInsertAsync(entities);
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task DeleteAsync(TKey id)
    {
        var entity = await GetAsync(id);
        _dbSet.Remove(entity);
    }

    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public async Task DeleteRangeAsync(IEnumerable<TKey> ids)
    {
        var entities = await GetAllByIdsAsync(ids);
        _dbSet.RemoveRange(entities);
    }

    public void BulkDelete(List<TEntity> entities)
    {
        _context.BulkDelete(entities);
    }

    public async Task BulkDeleteAsync(List<TEntity> entities)
    {
        await _context.BulkDeleteAsync(entities);
    }

    public bool Exist(TKey id)
    {
        return _dbSet.Any(p => p.Id.Equals(id));
    }

    public async Task<bool> ExistAsync(TKey id)
    {
        return await _dbSet.AnyAsync(p => p.Id.Equals(id));
    }

    public int Count(IEnumerable<TKey> ids)
    {
        return _dbSet.Count(p => ids.Contains(p.Id));
    }

    public async Task<int> CountAsync(IEnumerable<TKey> ids)
    {
        return await _dbSet.CountAsync(p => ids.Contains(p.Id));
    }
}