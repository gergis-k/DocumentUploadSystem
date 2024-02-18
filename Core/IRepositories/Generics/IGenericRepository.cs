using Core.Entities.Bases;

namespace Core.IRepositories.Generics;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> AddAsync(TEntity entity);

    IQueryable<TEntity> GetAllAsNoTracking();

    IQueryable<TEntity> GetAll();

    Task<TEntity?> GetByIdAsNoTrackingAsync(int id);

    TEntity? GetByIdAsNoTracking(int id);

    Task<TEntity?> GetByIdAsync(int id);

    TEntity? GetById(int id);

    Task<bool> UpdateAsync(TEntity entity);

    Task<bool> RemoveAsync(TEntity entity);

    Task<bool> RemoveByIdAsync(int id);
}
