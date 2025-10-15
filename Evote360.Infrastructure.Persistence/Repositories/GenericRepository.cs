using Evote360.Core.Domain.Interfaces;
using Evote360.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Evote360.Infrastructure.Persistence.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity>  where TEntity : class
{
    protected readonly VoteContext Context;

    public GenericRepository(VoteContext context)
    {
        Context = context;
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await Context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await Context.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity?> AddAsync(TEntity entity)
    {
         await Context.Set<TEntity>().AddAsync(entity);
         await Context.SaveChangesAsync();
         return entity;
    }

    public async Task<List<TEntity>> AddRangeAsync(List<TEntity> entities)
    {
        await Context.Set<TEntity>().AddRangeAsync(entities);
        await Context.SaveChangesAsync();
        return entities.ToList();
    }

    public async Task<TEntity?> UpdateAsync(int id, TEntity entity)
    {
        var entityDb = await Context.Set<TEntity>().FindAsync(id);
        if (entityDb != null)
        {
            Context.Entry(entityDb).CurrentValues.SetValues(entity);
            await Context.SaveChangesAsync();
            return entityDb;
        }
        return null;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await Context.Set<TEntity>().FindAsync(id);
        if (entity != null)
        {
            Context.Set<TEntity>().Remove(entity);
            await Context.SaveChangesAsync();
        }
    }

    public IQueryable<TEntity> GetAllQueryable()
    {
        return Context.Set<TEntity>().AsQueryable();
    }
}