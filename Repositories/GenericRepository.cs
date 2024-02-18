using Core.Entities.Bases;
using Core.IRepositories.Generics;
using Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private protected readonly SystemDatabase systemDatabase;

    public GenericRepository(SystemDatabase systemDatabase)
    {
        this.systemDatabase = systemDatabase;
    }

    public async Task<TEntity?> AddAsync(TEntity entity)
    {
        var addingResault = await systemDatabase.Set<TEntity>().AddAsync(entity);
        var resault = await systemDatabase.SaveChangesAsync();
        return resault > 0 ? addingResault.Entity : null;
    }

    public IQueryable<TEntity> GetAll()
        => systemDatabase.Set<TEntity>().AsTracking();

    public IQueryable<TEntity> GetAllAsNoTracking()
        => systemDatabase.Set<TEntity>().AsNoTracking();

    public TEntity? GetById(int id)
        => systemDatabase.Set<TEntity>().Find(id);

    public TEntity? GetByIdAsNoTracking(int id)
        => systemDatabase.Set<TEntity>().AsNoTracking().SingleOrDefault(e => e.Id == id);

    public async Task<TEntity?> GetByIdAsNoTrackingAsync(int id)
        => await systemDatabase.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(e => e.Id == id);

    public async Task<TEntity?> GetByIdAsync(int id)
        => await systemDatabase.Set<TEntity>().FindAsync(id);

    public async Task<bool> RemoveAsync(TEntity entity)
    {
        systemDatabase.Set<TEntity>().Remove(entity);
        var resault = await systemDatabase.SaveChangesAsync();
        return resault > 0;
    }

    public async Task<bool> RemoveByIdAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        return entity is not null && await RemoveAsync(entity);
    }

    public async Task<bool> UpdateAsync(TEntity entity)
    {
        systemDatabase.Set<TEntity>().Update(entity);
        var resault = await systemDatabase.SaveChangesAsync();
        return resault > 0;
    }
}
